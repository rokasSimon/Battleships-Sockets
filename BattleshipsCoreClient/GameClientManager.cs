using BattleshipsCore.Data;
using BattleshipsCore.Game;
using System.Net;

namespace BattleshipsCoreClient
{
    internal class GameClientManager
    {
        private static readonly object _lock = new();
        private static GameClientManager? _instance;

        private GameClientManager() { }

        public AsyncSocketClient? Client { get; private set; }
        public GameSessionData? ActiveSession { get; set; }
        public string? PlayerName { get; set; }

        public static GameClientManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new GameClientManager();
                    }
                }

                return _instance;
            }
        }

        public async Task<bool> EstablishClient(IPAddress ip)
        {
            if (Client != null)
            {
                await Client.DisconnectAsync();
            }

            Client = new AsyncSocketClient(ip, new GameMessageParser());

            var establishedConnection = await Client.ConnectAsync();
            if (!establishedConnection) return false;

            return true;
        }

        public async Task<bool> DisconnectAsync()
        {
            if (Client == null || PlayerName == null) return false;

            await Client.SendMessageAsync(new DisconnectRequest(PlayerName));
            await Client.DisconnectAsync();
            PlayerName = null;
            Client = null;

            return true;
        }

        public async Task LeaveSessionAsync()
        {
            if (Client == null || ActiveSession == null) return;

            await Client.SendMessageAsync(new LeaveSessionRequest(ActiveSession.SessionKey, PlayerName!));
        }
    }
}
