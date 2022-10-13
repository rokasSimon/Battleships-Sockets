using BattleshipsCore.Data.ShipBuiilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Ship.ShipBuiilder
{
    internal class LargeShipBuilder : ShipBuilder
    {
        public LargeShipBuilder()
        {
            ship = new LargeShip("large", 1, 3);
        }

        public override ShipBuilder BuildShootingRange()
        {
            ship.ShootingRange = 6;
            return this;
        }

        public override ShipBuilder BuildWeapons()
        {
            ship.NavalArtillery = 12;
            return this;
        }
    }
}
