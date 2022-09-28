using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.GameGrid
{
    internal class Tile
    {
        public TileType Type { get; init; }
        public Vec2 Position { get; init; }
        public object? OccupyingUnit { get; set; }

        public Tile(TileType type, Vec2 pos)
        {
            Type = type;
            Position = pos;
        }
    }
}
