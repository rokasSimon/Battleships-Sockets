using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Data.Surface
{
    public class Grass : Tile
    {
        public Grass(TileType type) : base(type)
        {
        }

        public  TileType Type => TileType.Grass;



        
    }
}
