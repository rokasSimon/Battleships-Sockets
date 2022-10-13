using BattleshipsCore.Data.ShipBuiilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Tank.TankBuilder
{
    internal class TankDirector
    {
        public ITank Construct(TankBuilder tankBuilder)
        {
            return tankBuilder.BuildShootingRange().BuildGroundWeapons().Build();
        }
    }
}
