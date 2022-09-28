using System.Net.Sockets;
using System.Net;
using System.Text;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Game;
using BattleshipsCore.Server;

namespace BattleshipsCoreClient
{
    public class AsyncSocketClient
    {
        private const int ListenerPort = 42069;
        private const int BufferSize = 1024;

        private readonly IPEndPoint _serverEndPoint;
        private readonly SocketStateData _clientSocketData;

        private readonly IMessageParser _commandFactory;

        public AsyncSocketClient(IPAddress ipAddress, IMessageParser commandFactory)
        {
            _commandFactory = commandFactory;
            _serverEndPoint = new IPEndPoint(ipAddress, ListenerPort);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clientSocketData = new SocketStateData(socket);
        }

        public bool Connect(string name)
        {
            _clientSocketData.Socket.Connect(_serverEndPoint);

            return SendCommand<JoinServerRequest, OkResponse>(new JoinServerRequest(name)) != null;
        }

        public void Disconnect(string name)
        {
            SendCommand<DisconnectRequest, OkResponse>(new DisconnectRequest(name));
        }

        public TResponse? SendCommand<TRequest, TResponse>(TRequest request)
            where TRequest : Request
            where TResponse : BattleshipsCore.Interfaces.Message
        {
            try
            {
                var result = Task.Run(() =>
                {
                    return SendCommandUnsafe<TRequest, TResponse>(request);
                }).Result;

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TResponse?> SendCommandAsync<TRequest, TResponse>(TRequest request)
            where TRequest : Request
            where TResponse : BattleshipsCore.Interfaces.Message
        {
            try
            {
                return await SendCommandUnsafe<TRequest, TResponse>(request);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<TResponse?> SendCommandUnsafe<TRequest, TResponse>(TRequest request)
            where TRequest : Request
            where TResponse : BattleshipsCore.Interfaces.Message
        {
            var commandMessage = _commandFactory.SerializeMessage(request);

            var response = await Send(commandMessage);

            var parsedResponse = _commandFactory.ParseResponse<TResponse>(response!);

            return parsedResponse;
        }

        private async Task<string?> Send(string message)
        {
            var data = Encoding.UTF8.GetBytes(message);
            await _clientSocketData.Socket.SendAsync(data, SocketFlags.None);

            return await ReceiveMessage();
        }

        private async Task<string?> ReceiveMessage()
        {
            var buffer = new byte[BufferSize];

            try
            {
                var bytesReceived = await _clientSocketData.Socket.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
    }
}
