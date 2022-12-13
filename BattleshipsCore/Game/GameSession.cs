using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using System.Collections.Generic;

namespace BattleshipsCore.Game
{
    internal class GameSession
    {
        private readonly Dictionary<string, PlayerMapInstance> _playerMaps;
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
            _playerMaps = new Dictionary<string, PlayerMapInstance>();

            Join(initiator);
        }

        public bool Join(PlayerData player)
        {
            if (Active || BattleActive ) return false;

            // TODO: check
            if(_players.ContainsKey(player.Name)) {
                player.JoinedSession = this;
                return true;
            }

            var isFirstPlayer = _players.Count == 0;

            player.JoinedSession = this;
            _players.Add(player.Name, player);
            _playerMaps.Add(player.Name, GenerateNewMap(isFirstPlayer));

            return true;
        }

        public bool Leave(PlayerData player)
        {
            if (!_players.ContainsKey(player.Name)) return false;

            var p = _players[player.Name];
            p.JoinedSession = null;

            _players.Remove(player.Name);
            _playerMaps.Remove(player.Name);

            return true;
        }

        public bool StartSession()
        {
            if (_players.Count == 2 && !Active)
            {
                Active = true;
                BattleActive = false;

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
            if (_playerMaps.TryGetValue(playerName, out var playerGameState))
            {
                var newSet = new Tile[playerGameState.Size.X, playerGameState.Size.Y];
                CopyGrid(playerGameState.OriginalGrid, newSet);

                foreach (var obj in objectsToPlace)
                {
                    foreach (var tile in obj.Tiles)
                    {
                        newSet[tile.X, tile.Y].Type = obj.Obj.Type;
                    }
                }

                //playerGameState.TilesToHit = objectsToPlace.SelectMany(x => x.Tiles).Count();
                //playerGameState.Grid = newSet;
                playerGameState.State.SetTiles(newSet);

                return true;
            }

            return false;
        }

        public bool UnsetMapFor(string playerName)
        {
            if (_playerMaps.TryGetValue(playerName, out var playerGameState))
            {
                return playerGameState.UnsetMap();
            }

            return false;
        }

        // Do not use if more than 2 players can play a game
        public GameMapData? GetOpponentMap(string playerName)
        {
            var map = GetOpponentMapValue(playerName);

            if (map == null) return null;

            return new GameMapData { Grid = map.OriginalGrid, Active = map.State.CanAct };
        }

        public bool StartBattle()
        {
            if (_players.Count != 2) return false;

            var success = _playerMaps.Values.All(x => x.State.StartBattle());

            if (success)
            {
                BattleActive = true;
                return true;
            }

            return false;
        }

        //public (GameState, List<TileUpdate>) GetTurnFor(string playerName)
        //{
        //    if (_playerMaps.TryGetValue(playerName, out var gameData))
        //    {
        //        var gameState = gameData.State is ActiveTurnState a
        //            ? GameState.YourTurn
        //            : GameState.EnemyTurn;

        //        return (gameData.GameState, gameData.TileToUpdate);
        //    }

        //    return (GameState.Unknown, new List<TileUpdate>());
        //}

        public (bool, List<TileUpdate>) Shoot(string playerName, List<Vec2> positions)
        {
            if (!_players.ContainsKey(playerName)) throw new ArgumentException("No such player exists");

            var myMap = _playerMaps[playerName];
            if (!myMap.State.CanAct) throw new ArgumentException("Not your turn");
            if (myMap.State.GameOver) throw new ArgumentException("Game already ended");

            var opponentMapData = GetOpponentMapValue(playerName);
            if (opponentMapData == null) throw new ArgumentException("Missing enemy player");

            var updates = myMap.State.ShootPlayer(positions, opponentMapData.State);

            return (myMap.State.GameOver, updates);
        }

        private PlayerMapInstance? GetOpponentMapValue(string playerName)
        {
            foreach (var item in _playerMaps)
            {
                if (item.Key != playerName) return item.Value;
            }

            return null;
        }

        private PlayerMapInstance GenerateNewMap(bool goFirst)
        {
            return new PlayerMapInstance(new(Constants.GridRowCount, Constants.GridColumnCount), goFirst);
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
    }
}
