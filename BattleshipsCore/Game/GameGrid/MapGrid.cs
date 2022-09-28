using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.GameGrid
{
    internal class MapGrid
    {
        public Vec2 Size { get; init; }
        public Tile[,] Grid { get; init; }

        public MapGrid(Vec2 size)
        {
            Size = size;
            Grid = new Tile[size.X, size.Y];
        }
    }
}
