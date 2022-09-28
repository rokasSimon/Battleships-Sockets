using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game
{
    internal class GameSession
    {
        private readonly Dictionary<string, MapGrid> _playerMaps;
        private readonly Dictionary<string, PlayerData> _players;

        public bool Active { get; private set; }
        public string SessionName { get; init; }

        public List<string> PlayerNames => _players.Keys.ToList();

        public GameSession(PlayerData initiator, string sessionName)
        {
            Active = false;
            SessionName = sessionName;

            _players = new Dictionary<string, PlayerData>();
            _playerMaps = new Dictionary<string, MapGrid>();

            Join(initiator);
        }

        public bool Join(PlayerData player)
        {
            if (Active || _players.ContainsKey(player.Name)) return false;

            player.JoinedSession = this;
            _players.Add(player.Name, player);

            return true;
        }

        public bool Leave(PlayerData player)
        {
            if (!_players.ContainsKey(player.Name)) return false;

            _players.Remove(player.Name);

            return true;
        }

        public bool StartSession()
        {
            if (_players.Count == 2 && !Active)
            {
                Active = true;

                foreach (var player in _players)
                {
                    var generatedMap = GenerateNewMap();

                    _playerMaps.Add(player.Key, generatedMap);
                }

                return true;
            }

            return false;
        }

        public void StopSession()
        {
            Active = false;

            foreach (var player in _players)
            {
                player.Value.JoinedSession = null;
            }

            _playerMaps.Clear();
            _players.Clear();
        }

        public GameMapData? GetMapFor(string name)
        {
            if (!_players.ContainsKey(name)) return null;

            var map = _playerMaps[name];

            return new GameMapData
            {
                Grid = map.Grid,
            };
        }

        private MapGrid GenerateNewMap()
        {
            return new MapGrid(new(15, 15));
        }
    }
}
