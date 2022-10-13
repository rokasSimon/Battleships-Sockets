using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Tank.TankBuilder
{
    internal class LargeTankeBuilder : TankBuilder
    {
        public LargeTankeBuilder()
        {
            tank = new LargeTank("large", 1, 3);
        }

        public override TankBuilder BuildShootingRange()
        {
            tank.ShootingRange = 4;
            return this;
        }

        public override TankBuilder BuildGroundWeapons()
        {
            tank.GroundArtillery = 3;
            return this;
        }
    }
}
