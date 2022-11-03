using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BattleshipsCore.Game.PlaceableObjects.Builder
{
    public class OneSailShipBuilder : ShipBuilder
    {
        public OneSailShipBuilder(int type, string name, int length, int max)
        {
            ship = new OneSailShip(length, name, max, type);
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
