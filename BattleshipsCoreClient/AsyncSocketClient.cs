using System.Net.Sockets;
using System.Net;
using System.Text;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Server;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Observer;
using Message = BattleshipsCore.Interfaces.Message;
using BattleshipsCoreClient.Mediator;

namespace BattleshipsCoreClient
{
    public class AsyncSocketClient : IMessagePublisher
    {
        private const int ListenerPort = 42069;
        private const int BufferSize = 8000;

        private readonly IPEndPoint _serverEndPoint;
        private readonly SocketStateData _clientSocketData;

        private readonly IMessageParser _commandFactory;

        private readonly List<ISubscriber> _subscribers;

        private bool _isListening;
        public ChatRoom Chatroom { set; get; }

        public string? PlayerName { get; set; }

        public AsyncSocketClient(IPAddress ipAddress, IMessageParser commandFactory)
        {
            _commandFactory = commandFactory;
            _serverEndPoint = new IPEndPoint(ipAddress, ListenerPort);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _clientSocketData = new SocketStateData(socket);
            _subscribers = new List<ISubscriber>();
            _isListening = false;
        }

        public async Task<bool> ConnectAsync()
        {
            try
            {
                await _clientSocketData.Socket.ConnectAsync(_serverEndPoint);

                Listen();

                return _clientSocketData.Socket.Connected == true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                await _clientSocketData.Socket.DisconnectAsync(false);
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task SendMessageAsync<TMessage>(TMessage message)
            where TMessage : Message
        {
            try
            {
                await SendMessageUnsafeAsync(message);
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task Notify(AcceptableResponse message)
        {
            for (int i = _subscribers.Count - 1; i >= 0; i--)
            {
                await _subscribers[i].UpdateAsync(message);
            }
        }

        public void Subscribe(ISubscriber listener)
        {
            _subscribers.Add(listener);
        }

        public void Unsubscribe(ISubscriber listener)
        {
            _subscribers.Remove(listener);
        }

        private void Listen()
        {
            _isListening = true;

            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (_isListening)
                {
                    var response = await ReceiveMessage();

                    if (response == null)
                    {
                        _isListening = false;
                        return;
                    }

                    try
                    {
                        var parsedMessage = _commandFactory.ParseResponse<AcceptableResponse>(response);

                        await Notify(parsedMessage);
                    }
                    catch (Exception)
                    {
                        var messages = TrySplittingMessage(response);

                        foreach (var msg in messages)
                        {
                            await Notify(msg);
                        }
                    }
                }
            }).Start();
        }

        private async Task SendMessageUnsafeAsync<TMessage>(TMessage message)
            where TMessage : Message
        {
            var messageData = _commandFactory.SerializeMessage(message);
            var data = Encoding.UTF8.GetBytes(messageData);

            await _clientSocketData.Socket.SendAsync(data, SocketFlags.None);
        }

        private List<AcceptableResponse> TrySplittingMessage(string response)
        {
            var messages = new List<AcceptableResponse>();

            var splits = response.Split("<EOF>", StringSplitOptions.RemoveEmptyEntries);

            foreach (var msg in splits)
            {
                messages.Add(_commandFactory.ParseResponse<AcceptableResponse>(msg));
            }

            return messages;
        }

        private async Task<string?> ReceiveMessage()
        {
            var response = string.Empty;
            var buffer = new byte[BufferSize];

            try
            {
                const string MessageEnding = "<EOF>";

                while (!response.Contains(MessageEnding))
                {
                    var bytesReceived = await _clientSocketData.Socket.ReceiveAsync(buffer, SocketFlags.None);
                    response += Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                }

                return response[0..^MessageEnding.Length];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public void Send(string to, string message)
        {
            Chatroom.Send(PlayerName, to, message);
        }

        public void Receive(string from, string message)
        {
            MessageBox.Show("Message from " + from + " to " + PlayerName + ": '" + message + "'");
        }
    }
}
