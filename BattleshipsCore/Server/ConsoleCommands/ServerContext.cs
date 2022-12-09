using BattleshipsCore.Game;

namespace BattleshipsCore.Server.ConsoleCommands
{
    internal class ServerContext
    {
        private readonly AsyncSocketServer _server;
        private readonly Dictionary<string, object> _variables;

        public ServerContext(AsyncSocketServer server)
        {
            _server = server;
            _variables = new Dictionary<string, object>();
        }

        public object? ReadVariable(string name)
        {
            if (name.Equals("players", StringComparison.OrdinalIgnoreCase))
            {
                return Players;
            }
            else if (name.Equals("sessions", StringComparison.OrdinalIgnoreCase))
            {
                return Sessions;
            }
            else if (_variables.ContainsKey(name))
            {
                return _variables[name];
            }
            else
            {
                return null;
            }
        }

        public void SetVariable(string name, object value)
        {
            _variables.Add(name, value);
        }

        public IEnumerable<string> SetVariablesFromProperties(object item)
        {
            var setVariables = new List<string>();

            if (item is PlayerData pd)
            {
                _variables.Add("name", pd.Name);
                setVariables.Add("name");

                if (pd.JoinedSession is not null)
                {
                    _variables.Add("session", pd.JoinedSession);
                    setVariables.Add("session");
                }
            }
            else if (item is GameSession session)
            {
                _variables.Add("name", session.SessionName);
                _variables.Add("isActive", session.Active);
                _variables.Add("isBattleActive", session.BattleActive);
                _variables.Add("playerNames", session.PlayerNames);

                setVariables.Add("name");
                setVariables.Add("isActive");
                setVariables.Add("isBattleActive");
                setVariables.Add("playerNames");
            }

            return setVariables;
        }

        public void RemoveVariable(string name)
        {
            _variables.Remove(name);
        }

        public static IEnumerable<PlayerData> Players => ServerGameStateManager.Instance.GetConnectedPlayers();
        public static IEnumerable<GameSession> Sessions => ServerGameStateManager.Instance.GetSessions();

        public void Disconnect(IEnumerable<PlayerData> players)
        {
            foreach (var player in players)
            {
                ServerGameStateManager.Instance.RemovePlayer(player.Name);
                player.SocketData.Socket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                player.SocketData.Socket.Close();
            }
        }

        public void Print(string text)
        {
            ServerLogger.Instance.LogInfo(text);
        }

        public void Broadcast(IEnumerable<PlayerData> players, string text)
        {
            foreach (var player in players)
            {
                _server.SendText(new Responses.SendTextResponse(text), player.SocketData);
            }
        }
    }
}
