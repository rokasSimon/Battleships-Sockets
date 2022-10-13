using BattleshipsCore.Game.PlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Data.ShipBuiilder
{
    abstract class ShipBuilder
    {
        protected IShip ship;
        public IShip Ship
        {
            get { return ship; }
        }
        public abstract ShipBuilder BuildShootingRange();
        public abstract ShipBuilder BuildWeapons();

        public IShip Build() {
            return ship;
        }
    }
}
