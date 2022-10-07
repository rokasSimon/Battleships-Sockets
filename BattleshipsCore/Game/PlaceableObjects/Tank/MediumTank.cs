using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Tank
{
    public class MediumTank : ITank
    {
        private int size = 2;
        private string name = "Medium Tank";

        public MediumTank(string name, int max, int length) : base(name, max, length)
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
