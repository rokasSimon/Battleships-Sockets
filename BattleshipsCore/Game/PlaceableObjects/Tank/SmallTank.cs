using BattleshipsCore.Game.GameGrid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Tank
{
    public class SmallTank : ITank
    {
        [JsonProperty("t")]

        public override TileType Type => TileType.Tank;
        private int size = 1;
        private string name = "Small Tank";

        public SmallTank(string name, int max, int length) : base(name, max, length)
        {
        }

        public override IShip GetShip(string name)
        {
            throw new NotImplementedException();
        }

        public override ITank GetTank(string name)
        {
            throw new NotImplementedException();
        }
    }
}
