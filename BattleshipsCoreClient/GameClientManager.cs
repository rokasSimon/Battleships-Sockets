using BattleshipsCore.Data;
using BattleshipsCore.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient
{
    internal class GameClientManager
    {
        private static readonly object _lock = new();
        private static GameClientManager? _instance;

        private GameClientManager()
        {

        }

        public AsyncSocketClient? Client { get; private set; }
        public GameSessionData? ActiveSession { get; private set; }
        public string? PlayerName { get; private set; }

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

        public bool Connect(IPAddress ip, string name)
        {
            if (Client != null && PlayerName != null) Client.Disconnect(PlayerName);

            PlayerName = name;
            Client = new AsyncSocketClient(ip, new GameMessageParser());

            return Client.Connect(PlayerName!);
        }

        public bool Disconnect()
        {
            if (Client == null || PlayerName == null) return false;

            Client.Disconnect(PlayerName);
            PlayerName = null;
            Client = null;

            return true;
        }

        public GameSessionData? CreateSession(string sessionName)
        {
            if (Client == null) return null;

            var createSessionResponse = Client!.SendCommand<CreateSessionRequest, SendSessionKeyResponse>(
                    new CreateSessionRequest(PlayerName!, sessionName));

            if (createSessionResponse == null) return null;

            var sessionData = new GameSessionData { SessionKey = createSessionResponse.SessionKey, SessionName = sessionName, PlayerNames = new List<string> { PlayerName! }, Active = false };

            ActiveSession = sessionData;

            return sessionData;
        }

        public bool JoinSession(GameSessionData session)
        {
            if (Client == null) return false;

            var success = Client.SendCommand<JoinSessionRequest, OkResponse>(new JoinSessionRequest(session.SessionKey, PlayerName!)) != null;

            if (success)
            {
                ActiveSession = session;

                return true;
            }

            return false;
        }

        public bool LeaveSession()
        {
            if (Client == null || ActiveSession == null) return false;

            var success = Client.SendCommand<LeaveSessionRequest, OkResponse>(new LeaveSessionRequest(ActiveSession.SessionKey, PlayerName!)) != null;

            if (success)
            {
                ActiveSession = null;

                return true;
            }

            return false;
        }
    }
}
