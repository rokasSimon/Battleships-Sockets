using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects.Tank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Ship
{
    public class MediumShip : IShip
    {
        public int size = 2;
        public string name = "Medium Ship";

        public MediumShip(string name, int max, int length) : base(name, max, length)
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
