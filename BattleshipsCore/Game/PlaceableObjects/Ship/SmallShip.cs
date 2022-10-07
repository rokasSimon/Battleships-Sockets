using BattleshipsCore.Game.GameGrid;
using System;
using BattleshipsCore.Game.PlaceableObjects.Tank;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Ship
{
    public class SmallShip : IShip
    {
        public int size = 1;
        public string name = "Small ship";

        public SmallShip(string name, int max, int length) : base(name, max, length)
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
