using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Tank.TankBuilder
{
    internal class MediumTankBuilder : TankBuilder
    {
        public MediumTankBuilder()
        {
            tank = new MediumTank("medium", 2, 2);
        }

        public override TankBuilder BuildShootingRange()
        {
            tank.ShootingRange = 3;
            return this;
        }

        public override TankBuilder BuildGroundWeapons()
        {
            tank.GroundArtillery = 2;
            return this;
        }
    }
}
