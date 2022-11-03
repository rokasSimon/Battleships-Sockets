using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;
using System.Collections.Generic;

namespace BattleshipsCore.Game
{
    internal class GameSession
    {
        private readonly Dictionary<string, PlayerGameState> _playerMaps;
        private readonly Dictionary<string, PlayerData> _players;

        public bool Active { get; private set; }
        public bool BattleActive { get; set; }
        public string SessionName { get; init; }

        public int Level { get; set; }

        public List<string> PlayerNames => _players.Keys.ToList();
        public List<AllowedUnitData> AllowedPlaceableObjects
        {
            get
            {
                var allowedUnits = new List<AllowedUnitData>();

                if (Level == 0)
                {
                    allowedUnits.Add(new AllowedUnitData { Type = TileType.Ship, Name = "Frigate", Length = 2, Max = 3, SideBlocks = 0 });
                    allowedUnits.Add(new AllowedUnitData { Type = TileType.Ship, Name = "Destroyer", Length = 4, Max = 2, SideBlocks = 1 });
                    allowedUnits.Add(new AllowedUnitData { Type = TileType.Tank, Name = "Infantry", Length = 1, Max = 2, SideBlocks = 0 });
                    allowedUnits.Add(new AllowedUnitData { Type = TileType.Tank, Name = "Cruiser", Length = 1, Max = 2, SideBlocks = 0 });
                }
                else if (Level == 1)
                {
                    allowedUnits.Add(new AllowedUnitData { Type = TileType.Ship, Name = "Carrier", Length = 4, Max = 1, SideBlocks = 2 });
                    allowedUnits.Add(new AllowedUnitData { Type = TileType.Tank, Name = "Amphibious", Length = 1, Max = 2, SideBlocks = 0 });
                }

                return allowedUnits;
            }
        }

        public GameSession(PlayerData initiator, string sessionName)
        {
            Active = false;
            BattleActive = false;
            SessionName = sessionName;
            Level = 0;

            _players = new Dictionary<string, PlayerData>();
            _playerMaps = new Dictionary<string, PlayerGameState>();

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

            player.JoinedSession = this;
            _players.Add(player.Name, player);

            return true;
        }

        public bool Leave(PlayerData player)
        {
            if (!_players.ContainsKey(player.Name)) return false;

            var p = _players[player.Name];
            p.JoinedSession = null;

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

                playerGameState.TilesToHit = objectsToPlace.SelectMany(x => x.Tiles).Count();
                playerGameState.Grid = newSet;

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

            return new GameMapData { Grid = map.OriginalGrid };
        }

        public bool StartBattle()
        {
            if (_players.Count != 2 || !_playerMaps.Values.All(x => x.Grid != null)) return false;

            BattleActive = true;
            _playerMaps.Values.First().GameState = GameState.YourTurn;

            return true;
        }

        public (GameState, List<TileUpdate>) GetTurnFor(string playerName)
        {
            if (_playerMaps.TryGetValue(playerName, out var gameData))
            {
                return (gameData.GameState, gameData.TileToUpdate);
            }

            return (GameState.Unknown, new List<TileUpdate>());
        }

        public (GameState, List<TileUpdate>) Shoot(string playerName, List<Vec2> positions)
        {
            if (!_players.ContainsKey(playerName)) return (GameState.Unknown, new List<TileUpdate>());

            var myMap = _playerMaps[playerName];
            if (myMap.GameState == GameState.EnemyTurn) return (GameState.Unknown, new List<TileUpdate>());
            if (myMap.GameState == GameState.Lost || myMap.GameState == GameState.Won) return (myMap.GameState, new List<TileUpdate>());

            var opponentMapData = GetOpponentMapValue(playerName);
            if (opponentMapData == null) return (GameState.Unknown, new List<TileUpdate>());

            var list = new List<TileUpdate>();

            foreach (var position in positions)
            {
                if(!position.IsInsideGrid(opponentMapData.Size)) return (GameState.Unknown, new List<TileUpdate>());

                var shotTile = opponentMapData.Grid![position.X, position.Y].Type;
                var newTileType = shotTile switch
                {
                    TileType.Ship => TileType.Hit,
                    TileType.Tank => TileType.Hit,
                    TileType.Hit => TileType.Hit,
                    _ => TileType.Miss,
                };

                if (newTileType == TileType.Hit && shotTile != TileType.Hit)
                {
                    opponentMapData.TilesToHit--;
                }

                opponentMapData.Grid![position.X, position.Y].Type = newTileType;
                var tileUpdate = new TileUpdate(position, newTileType);
                list.Add(tileUpdate);
            }

            if (opponentMapData.TilesToHit == 0)
            {
                opponentMapData.GameState = GameState.Lost;
                myMap.GameState = GameState.Won;

                Level++;
            }
            else
            {
                opponentMapData.TileToUpdate = list;
                opponentMapData.GameState = GameState.YourTurn;
                myMap.GameState = GameState.EnemyTurn;
            }

            return (myMap.GameState, list);
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
            var gameState = new PlayerGameState(new(Constants.GridRowCount, Constants.GridColumnCount));
            var map = gameState.OriginalGrid;

            for (int i = 0; i < Constants.GroundTileCount; i++)
            {
                var randomX = Random.Shared.Next(0, Constants.GridRowCount);
                var randomY = Random.Shared.Next(0, Constants.GridColumnCount);

                map[randomX, randomY] = new Tile(TileType.Ground);
            }

            return gameState;
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
