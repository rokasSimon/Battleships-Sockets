using BattleshipsCore.Data.ShipBuiilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Ship.ShipBuiilder
{
    internal class MediumShipBuilder : ShipBuilder
    {
        public MediumShipBuilder()
        {
            ship = new MediumShip("medium", 2, 2);
        }

        public override ShipBuilder BuildShootingRange()
        {
            ship.ShootingRange = 4;
            return this;
        }

        public override ShipBuilder BuildWeapons()
        {
            ship.NavalArtillery = 8;
            return this;
        }
    }
}
