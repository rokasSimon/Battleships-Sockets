using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Server;
using System.Collections.Generic;

namespace BattleshipsCore.Game
{
    internal abstract class PlayerGameState
    {
        public PlayerMapInstance PlayerInstance { get; set; }

        public PlayerGameState(PlayerMapInstance instance)
        {
            PlayerInstance = instance;
        }

        public abstract bool CanAct { get;  }
        public abstract bool GameOver { get; }
        public abstract Tile[,]? UnsetMap();
        public abstract void SetTiles(Tile[,] newTiles);
        public abstract List<TileUpdate> ShootPlayer(List<Vec2> positions, PlayerGameState otherPlayer);
        public abstract bool StartBattle();
        public abstract (int, List<TileUpdate>) TakeDamage(List<Vec2> positions);
    }

    internal class JoinedState : PlayerGameState
    {
        public bool IsFirst { get; set; }
        public override bool CanAct => false;
        public override bool GameOver => false;

        public JoinedState(PlayerMapInstance instance, bool isFirst)
            : base(instance)
        {
            IsFirst = isFirst;
        }

        public override bool StartBattle()
        {
            return false;
        }

        public override (int, List<TileUpdate>) TakeDamage(List<Vec2> positions)
        {
            return (0, new List<TileUpdate>());
        }

        public override void SetTiles(Tile[,] newTiles)
        {
            PlayerInstance.State = new SettingState(PlayerInstance, IsFirst);
            PlayerInstance.State.SetTiles(newTiles);
        }

        public override Tile[,]? UnsetMap()
        {
            return null;
        }

        public override List<TileUpdate> ShootPlayer(List<Vec2> positions, PlayerGameState otherPlayer)
        {
            return new List<TileUpdate>();
        }
    }

    internal class SettingState : PlayerGameState
    {
        private DropoutStack<Tile[,]> _setMaps;
        private Tile[,]? _grid
        {
            get => _setMaps.Peek();
            set => _setMaps.Push(value);
        }

        public bool IsFirst { get; set; }
        public override bool GameOver => false;
        public override bool CanAct => false;

        public SettingState(PlayerMapInstance instance, bool isFirst)
            : base(instance)
        {
            IsFirst = isFirst;
            _setMaps = new DropoutStack<Tile[,]>(3);
        }

        public override bool StartBattle()
        {
            if (_grid == null) return false;

            if (IsFirst)
            {
                PlayerInstance.State = new ActiveTurnState(PlayerInstance, _grid, CountSetTiles());
            }
            else
            {
                PlayerInstance.State = new InactiveTurnState(PlayerInstance, _grid, CountSetTiles());
            }

            return true;
        }

        public override (int, List<TileUpdate>) TakeDamage(List<Vec2> positions)
        {
            return (0, new List<TileUpdate>());
        }

        public override void SetTiles(Tile[,] newTiles)
        {
            _grid = newTiles;
        }

        public override Tile[,]? UnsetMap()
        {
            return _setMaps.Pop();
        }

        public override List<TileUpdate> ShootPlayer(List<Vec2> positions, PlayerGameState otherPlayer)
        {
            return new List<TileUpdate>();
        }

        private int CountSetTiles()
        {
            var grid = _grid;
            int count = 0;

            for (int i = 0; i < grid!.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    var type = grid[i, j].Type;

                    switch (type)
                    {
                        case TileType.Tank:
                        case TileType.Tanker:
                        case TileType.NarrowBoat:
                        case TileType.Cruise:
                        case TileType.Ship:
                            {
                                count++;
                                continue;
                            }
                        default: continue;
                    }
                }
            }

            return count;
        }
    }

    internal class ActiveTurnState : PlayerGameState
    {
        public int TilesToHit { get; set; }
        public Tile[,] Grid { get; set; }
        public override bool CanAct => true;
        public override bool GameOver => false;

        public ActiveTurnState(PlayerMapInstance instance, Tile[,] grid, int tilesLeft)
            : base(instance)
        {
            TilesToHit = tilesLeft;
            Grid = grid;
        }

        public override Tile[,]? UnsetMap()
        {
            return null;
        }

        public override void SetTiles(Tile[,] newTiles)
        {
            return;
        }

        public override bool StartBattle()
        {
            return false;
        }

        public override (int, List<TileUpdate>) TakeDamage(List<Vec2> positions)
        {
            return (0, new List<TileUpdate>());
        }

        public override List<TileUpdate> ShootPlayer(List<Vec2> positions, PlayerGameState otherPlayer)
        {
            var (tilesLeft, updates) = otherPlayer.TakeDamage(positions);

            if (tilesLeft == 0) PlayerInstance.State = new WinnerGameState(PlayerInstance);
            else PlayerInstance.State = new InactiveTurnState(PlayerInstance, Grid, TilesToHit);

            return updates;
        }
    }

    internal class InactiveTurnState : PlayerGameState
    {
        public int TilesToHit { get; set; }
        public Tile[,] Grid { get; set; }
        public override bool CanAct => false;
        public override bool GameOver => false;

        public InactiveTurnState(PlayerMapInstance instance, Tile[,] grid, int tilesLeft)
            : base(instance)
        {
            TilesToHit = tilesLeft;
            Grid = grid;
        }

        public override Tile[,]? UnsetMap()
        {
            return null;
        }

        public override void SetTiles(Tile[,] newTiles)
        {
            return;
        }

        public override bool StartBattle()
        {
            return false;
        }

        public override (int, List<TileUpdate>) TakeDamage(List<Vec2> positions)
        {
            var gridSize = PlayerInstance.Size;
            var updateList = new List<TileUpdate>();

            foreach (var position in positions)
            {
                if (!Vec2.InsideGrid(position, gridSize)) throw new ArgumentException("Position is outside grid");

                var shotTile = Grid![position.X, position.Y].Type;
                var newTileType = shotTile switch
                {
                    TileType.Ship => TileType.Hit,
                    TileType.Tank => TileType.Hit,
                    TileType.NarrowBoat => TileType.Hit,
                    TileType.Cruise => TileType.Hit,
                    TileType.Tanker => TileType.Hit,
                    TileType.Hit => TileType.Hit,

                    _ => TileType.Miss,
                };

                if (newTileType == TileType.Hit && shotTile != TileType.Hit)
                {
                    TilesToHit--;
                }

                Grid[position.X, position.Y].Type = newTileType;
                var tileUpdate = new TileUpdate(position, newTileType);
                updateList.Add(tileUpdate);
            }

            if (TilesToHit == 0)
            {
                PlayerInstance.State = new LoserGameState(PlayerInstance);
            }
            else
            {
                PlayerInstance.State = new ActiveTurnState(PlayerInstance, Grid, TilesToHit);
            }

            return (TilesToHit, updateList);
        }

        public override List<TileUpdate> ShootPlayer(List<Vec2> positions, PlayerGameState otherPlayer)
        {
            return new List<TileUpdate>();
        }
    }

    internal class WinnerGameState : PlayerGameState
    {
        public override bool CanAct => false;
        public override bool GameOver => true;

        public WinnerGameState(PlayerMapInstance instance)
            : base(instance)
        {
        }

        public override void SetTiles(Tile[,] newTiles)
        {
            throw new NotImplementedException();
        }

        public override bool StartBattle()
        {
            return false;
        }

        public override (int, List<TileUpdate>) TakeDamage(List<Vec2> positions)
        {
            return (0, new List<TileUpdate>());
        }

        public override Tile[,]? UnsetMap()
        {
            return null;
        }

        public override List<TileUpdate> ShootPlayer(List<Vec2> positions, PlayerGameState otherPlayer)
        {
            return new List<TileUpdate>();
        }
    }

    internal class LoserGameState : PlayerGameState
    {
        public override bool CanAct => false;
        public override bool GameOver => true;

        public LoserGameState(PlayerMapInstance instance)
            : base(instance)
        {
        }

        public override void SetTiles(Tile[,] newTiles)
        {
            return;
        }

        public override bool StartBattle()
        {
            return false;
        }

        public override (int, List<TileUpdate>) TakeDamage(List<Vec2> positions)
        {
            return (0, new List<TileUpdate>());
        }

        public override Tile[,]? UnsetMap()
        {
            return null;
        }

        public override List<TileUpdate> ShootPlayer(List<Vec2> positions, PlayerGameState otherPlayer)
        {
            return new List<TileUpdate>();
        }
    }
}
