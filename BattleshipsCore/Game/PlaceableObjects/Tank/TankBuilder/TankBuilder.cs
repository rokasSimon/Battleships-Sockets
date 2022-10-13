using BattleshipsCore.Data.ShipBuiilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Tank.TankBuilder
{
    abstract class TankBuilder
    {
        protected ITank tank;
        public ITank Tank
        {
            get { return tank; }
        }
        public abstract TankBuilder BuildShootingRange();
        public abstract TankBuilder BuildGroundWeapons();

        public ITank Build()
        {
            return tank;
        }
    }
}
