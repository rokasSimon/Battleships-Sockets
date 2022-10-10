using BattleshipsCore.Data;
using BattleshipsCore.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game
{
    internal class ServerGameStateManager
    {
        private static readonly object _lock = new();
        private static ServerGameStateManager? _instance;

        private ServerGameStateManager()
        {
            _players = new Dictionary<string, PlayerData>();
            _sessions = new Dictionary<Guid, GameSession>();
        }

        public static ServerGameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new ServerGameStateManager();
                    }
                }

                return _instance;
            }
        }

        private readonly Dictionary<string, PlayerData> _players;
        private readonly Dictionary<Guid, GameSession> _sessions;

        public bool TryAddPlayer(PlayerData player)
        {
            if (_players.ContainsKey(player.Name)) return false;

            _players.Add(player.Name, player);

            return true;
        }

        public bool TryRemovePlayer(string name)
        {
            if (!_players.ContainsKey(name)) return false;

            var player = _players[name];
            player.JoinedSession?.Leave(player);

            return _players.Remove(name);
        }

        public bool HasPlayer(string name)
        {
            return _players.ContainsKey(name);
        }

        public string[] GetAvailablePlayers(string? playerToExclude)
        {
            return _players
                .Select(x => x.Value.Name)
                .Where(x => playerToExclude == null || x != playerToExclude)
                .ToArray();
        }

        public GameSession? GetSession(Guid guid)
        {
            if (!_sessions.ContainsKey(guid)) return null;

            return _sessions[guid];
        }

        public PlayerData? GetPlayer(string name)
        {
            if (!_players.ContainsKey(name)) return null;

            return _players[name];
        }

        public PlayerData? GetConnectedPlayer(Guid connectionId)
        {
            return _players.Values.SingleOrDefault(x => x.SocketData?.Id == connectionId);
        }

        public Guid? TryCreateSession(string initiator, string sessionName)
        {
            if (!_players.ContainsKey(initiator)) return null;

            var session = new GameSession(_players[initiator], sessionName);
            var sessionKey = Guid.NewGuid();

            _sessions.Add(sessionKey, session);

            return sessionKey;
        }

        public List<GameSessionData> GetSessionList()
        {
            return _sessions
                .Select(x =>
                {
                    return new GameSessionData
                    {
                        SessionKey = x.Key,
                        SessionName = x.Value.SessionName,
                        PlayerNames = x.Value.PlayerNames,
                        Active = x.Value.Active,
                    };
                })
                .ToList();
        }

        public bool TryJoiningSession(Guid sessionId, string joiningPlayer)
        {
            var session = GetSession(sessionId);
            var player = GetPlayer(joiningPlayer);

            if (session != null && player != null)
            {
                return session.Join(player);
            }
            
            return false;
        }

        public bool TryLeavingSession(Guid sessionId, string leavingPlayer)
        {
            var session = GetSession(sessionId);
            var player = GetPlayer(leavingPlayer);

            if (session != null && player != null)
            {
                var result = session.Leave(player);

                if (session.PlayerNames.Count == 0)
                {
                    session.StopSession();
                    _sessions.Remove(sessionId);
                }

                return result;
            }

            return false;
        }
    }
}
