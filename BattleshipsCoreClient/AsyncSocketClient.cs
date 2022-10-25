using System.Net.Sockets;
using System.Net;
using System.Text;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Server;
using BattleshipsCore.Responses;
using BattleshipsCoreClient.Observer;
using Message = BattleshipsCore.Interfaces.Message;

namespace BattleshipsCoreClient
{
    public class AsyncSocketClient : MessagePublisher
    {
        private const int ListenerPort = 42069;
        private const int BufferSize = 4096;

        private readonly IPEndPoint _serverEndPoint;
        private readonly SocketStateData _clientSocketData;

        private readonly IMessageParser _commandFactory;

        private bool _isListening;

        public AsyncSocketClient(IPAddress ipAddress, IMessageParser commandFactory)
        {
            _commandFactory = commandFactory;
            _serverEndPoint = new IPEndPoint(ipAddress, ListenerPort);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            _clientSocketData = new SocketStateData(socket);
            _isListening = false;
        }

        private Message? _Message;

        public Message? GetMessage()
        {
            return _Message;
        }

        public async Task SetMessage(Message message)
        {
            _Message = message;
            await Notify();
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
                        var parsedMessage = _commandFactory.ParseResponse<Message>(response);

                        await SetMessage(parsedMessage);
                    }
                    catch (Exception)
                    {
                        var messages = TrySplittingMessage(response);

                        foreach (var msg in messages)
                        {
                            await SetMessage(msg);
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

        private List<Message> TrySplittingMessage(string response)
        {
            var messages = new List<Message>();

            var splits = response.Split("<EOF>", StringSplitOptions.RemoveEmptyEntries); //response.Replace("<EOF>", "\0");
            //var splits = separated.Split('\0');

            foreach (var msg in splits)
            {
                messages.Add(_commandFactory.ParseResponse<Message>(msg));
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
    }
}
