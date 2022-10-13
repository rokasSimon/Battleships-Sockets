using BattleshipsCore.Data.ShipBuiilder;
using BattleshipsCore.Game.PlaceableObjects.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Tank.TankBuilder
{
    internal class SmallTankBuilder : TankBuilder
    {
        public SmallTankBuilder()
        {
            tank = new SmallTank("small", 3, 1);
        }

        public override TankBuilder BuildShootingRange()
        {
            tank.ShootingRange = 2;
            return this;
        }

        public override TankBuilder BuildGroundWeapons()
        {
            tank.GroundArtillery = 1;
            return this;
        }
    }
}
