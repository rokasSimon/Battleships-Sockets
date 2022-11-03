using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Data.Surface
{
    public class Ground : Tile
    {
        public Ground(TileType type) : base(type)
        {
        }

        public TileType Type => TileType.Ground;


        

        
    }
}
