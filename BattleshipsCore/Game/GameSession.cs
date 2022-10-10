using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game
{
    internal class GameSession
    {
        private readonly Dictionary<string, PlayerGameState> _playerMaps;
        private readonly Dictionary<string, PlayerData> _players;

        public bool Active { get; private set; }
        public bool BattleActive { get; set; }
        public string SessionName { get; init; }

        public List<string> PlayerNames => _players.Keys.ToList();

        public GameSession(PlayerData initiator, string sessionName)
        {
            Active = false;
            BattleActive = false;
            SessionName = sessionName;

            _players = new Dictionary<string, PlayerData>();
            _playerMaps = new Dictionary<string, PlayerGameState>();

            Join(initiator);
        }

        public bool Join(PlayerData player)
        {
            if (Active || BattleActive || _players.ContainsKey(player.Name)) return false;

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
                BattleActive = false;

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
            BattleActive = false;

            foreach (var player in _players)
            {
                player.Value.JoinedSession = null;
            }

            _playerMaps.Clear();
            _players.Clear();
        }

        public GameMapData? GetMapFor(string playerName)
        {
            if (!_players.ContainsKey(playerName)) return null;

            var map = _playerMaps[playerName];

            return new GameMapData
            {
                Grid = map.OriginalGrid,
            };
        }

        public bool SetMapFor(string playerName, List<PlacedObject> objectsToPlace)
        {
            if (_playerMaps.TryGetValue(playerName, out var map))
            {
                map.Grid = new Tile[map.Size.X, map.Size.Y];
                CopyGrid(map.OriginalGrid, map.Grid);

                foreach (var obj in objectsToPlace)
                {
                    foreach (var tile in obj.Tiles)
                    {
                        map.Grid[tile.X, tile.Y].Type = obj.Obj.Type;
                    }
                }

                map.TilesToHit = objectsToPlace.SelectMany(x => x.Tiles).Count();

                return true;
            }

            return false;
        }

        // Do not use if more than 2 players can play a game
        public GameMapData? GetOpponentMap(string playerName)
        {
            var map = GetOpponentMapValue(playerName);

            if (map == null) return null;

            return new GameMapData { Grid = map.OriginalGrid };
        }

        public bool StartBattle()
        {
            if (_players.Count != 2 || !_playerMaps.Values.All(x => x.Grid != null)) return false;

            BattleActive = true;
            _playerMaps.Values.First().GameState = GameState.YourTurn;

            return true;
        }

        public (GameState, TileUpdate?) GetTurnFor(string playerName)
        {
            if (_playerMaps.TryGetValue(playerName, out var gameData))
            {
                return (gameData.GameState, gameData.TileToUpdate);
            }

            return (GameState.Unknown, null);
        }

        public (GameState, TileUpdate?) Shoot(string playerName, Vec2 position)
        {
            if (!_players.ContainsKey(playerName)) return (GameState.Unknown, null);

            var myMap = _playerMaps[playerName];
            if (myMap.GameState == GameState.EnemyTurn) return (GameState.Unknown, null);
            if (myMap.GameState == GameState.Lost || myMap.GameState == GameState.Won) return (myMap.GameState, null);

            var opponentMapData = GetOpponentMapValue(playerName);
            if (opponentMapData == null) return (GameState.Unknown, null);

            if (!IsInsideGrid(opponentMapData.Size, position)) return (GameState.Unknown, null);

            var shotTile = opponentMapData.Grid![position.X, position.Y].Type;
            var newTileType = shotTile switch
            {
                TileType.Ship => TileType.Hit,
                TileType.Hit => TileType.Hit,
                _ => TileType.Miss,
            };

            if (newTileType == TileType.Hit && shotTile != TileType.Hit)
            {
                opponentMapData.TilesToHit--;
            }

            opponentMapData.Grid![position.X, position.Y].Type = newTileType;
            var tileUpdate = new TileUpdate(position, newTileType);

            if (opponentMapData.TilesToHit == 0)
            {
                opponentMapData.GameState = GameState.Lost;
                myMap.GameState = GameState.Won;
            }
            else
            {
                opponentMapData.TileToUpdate = tileUpdate;
                opponentMapData.GameState = GameState.YourTurn;
                myMap.GameState = GameState.EnemyTurn;
            }

            return (myMap.GameState, tileUpdate);
        }

        private PlayerGameState? GetOpponentMapValue(string playerName)
        {
            foreach (var item in _playerMaps)
            {
                if (item.Key != playerName) return item.Value;
            }

            return null;
        }

        private PlayerGameState GenerateNewMap()
        {
            return new PlayerGameState(new(15, 15));
        }

        private static void CopyGrid(Tile[,] source, Tile[,] destination)
        {
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    destination[i, j] = source[i, j];
                }
            }
        }

        private static bool IsInsideGrid(Vec2 gridSize, Vec2 pos)
        {
            if (pos.X < 0
                || pos.Y < 0
                || pos.X >= gridSize.X
                || pos.Y >= gridSize.Y)
                return false;

            return true;
        }
    }
}
