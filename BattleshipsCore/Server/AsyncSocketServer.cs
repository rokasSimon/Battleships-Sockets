using System.Net;
using System.Text;
using System.Net.Sockets;
using Newtonsoft.Json;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Server
{
    public class AsyncSocketServer : IDisposable
    {
        private const int ListenerPort = 42069;
        private const int MaximumSocketQueueSize = 100;

        private readonly IPAddress _serverIpAddress;
        private readonly IPEndPoint _serverEndPoint;
        private readonly Socket _serverSocket;
        private readonly Dictionary<Guid, SocketStateData> _connectedClients;
        private readonly IMessageParser _commandFactory;

        public AsyncSocketServer(IPAddress ipAddress, IMessageParser commandParser)
        {
            _serverIpAddress = ipAddress;
            _serverEndPoint = new IPEndPoint(_serverIpAddress, ListenerPort);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _commandFactory = commandParser;
            _connectedClients = new Dictionary<Guid, SocketStateData>();
        }

        public void Run()
        {
            _serverSocket.Bind(_serverEndPoint);
            _serverSocket.Listen(MaximumSocketQueueSize);

            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                var clientSocket = _serverSocket.EndAccept(ar);

                var client = new SocketStateData(clientSocket);
                _connectedClients.Add(client.Id, client);

                client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);

                _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            }
            catch (Exception e)
            {
                ServerLogger.Instance.LogError(e.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var client = (SocketStateData)ar.AsyncState!;

            try
            {
                int bytesReceived = client.Socket.EndReceive(ar);

                var response = Encoding.UTF8.GetString(client.Buffer, 0, bytesReceived);
                ServerLogger.Instance.LogInfo($"Received message: '{response}';");

                var command = _commandFactory.ParseRequest<Request>(response);

                var responseTargets = command.Execute(client.Id);

                // Handling for connection based requests
                switch (command)
                {
                    case DisconnectRequest dr:
                        {
                            client.Socket.Shutdown(SocketShutdown.Both);
                            client.Socket.Close();
                        } return;
                    case JoinServerRequest jsr:
                        {
                            var player = ServerGameStateManager.Instance.GetPlayer(jsr.PlayerName);

                            if (player != null) player.SocketData = client;
                        } break;
                    default: break;
                }

                foreach (var (res, cl) in responseTargets)
                {
                    SendResponse(res, _connectedClients[cl]);
                }

                CheckForDisconnectedClients();

                client.Socket.BeginReceive(client.Buffer, 0, client.Buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), client);
            }
            catch (JsonSerializationException e)
            {
                ServerLogger.Instance.LogWarning(e.Message);

                SendFailForCatch(client);
            }
            catch (UnknownMessageException e)
            {
                ServerLogger.Instance.LogError(e.Message);

                SendFailForCatch(client);
            }
            catch (Exception e)
            {
                ServerLogger.Instance.LogError($"Caught exception: ({e.Message})");
            }
        }

        private void SendResponse(Message command, SocketStateData socketData)
        {
            var commandMessage = _commandFactory.SerializeMessage(command);

            Send(commandMessage, socketData);
        }

        private void Send(string message, SocketStateData socketData)
        {
            ServerLogger.Instance.LogInfo($"Sending response of '{message}';");

            var data = Encoding.UTF8.GetBytes(message + "<EOF>");

            socketData.Socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socketData);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (SocketStateData)ar.AsyncState!;
                client.Socket.EndSend(ar);
            }
            catch (Exception e)
            {
                ServerLogger.Instance.LogError(e.Message);
            }
        }

        private void SendFailForCatch(SocketStateData clientData)
        {
            try
            {
                SendResponse(new FailResponse(), clientData);
            }
            catch (Exception)
            {
                ServerLogger.Instance.LogError("Could not respond with error;");
            }
        }

        private void CheckForDisconnectedClients()
        {
            foreach (var client in _connectedClients)
            {
                var playerData = ServerGameStateManager.Instance.GetConnectedPlayer(client.Key);

                if (playerData == null)
                {
                    client.Value.Socket.Close();

                    ServerLogger.Instance.LogWarning($"Removing forcefully disconnected client: {client.Key};");

                    _connectedClients.Remove(client.Key);
                }
            }
        }

        public void Dispose()
        {
            _serverSocket.Close();
            GC.SuppressFinalize(this);
        }
    }
}
