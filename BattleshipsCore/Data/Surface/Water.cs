using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Data.Surface
{
    public class Water : Tile
    {
        public Water(TileType type) : base(type)
        {
        }

        public TileType Type => TileType.Water;
        
    }
}
