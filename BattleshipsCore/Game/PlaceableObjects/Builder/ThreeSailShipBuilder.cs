using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BattleshipsCore.Game.PlaceableObjects.Builder
{
    public class ThreeSailShipBuilder : ShipBuilder
    {
        public ThreeSailShipBuilder(int type, string name, int length, int max)
        {
            ship = new Brig(length, name, max, type);
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
