using BattleshipsCore.Game.PlaceableObjects.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Data.ShipBuiilder
{
    internal class SmallShipBuilder : ShipBuilder
    {
        public SmallShipBuilder()
        {
            ship = new SmallShip("small", 3, 1);
        }

        public override ShipBuilder BuildShootingRange()
        {
            ship.ShootingRange = 2;
            return this;
        }

        public override ShipBuilder BuildWeapons()
        {
            ship.NavalArtillery = 4;
            return this;
        }
    }
}
