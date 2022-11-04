using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BattleshipsCore.Game.PlaceableObjects.Builder
{
    public class TwoSailShipBuilder : ShipBuilder
    {
        public TwoSailShipBuilder(int type, string name, int length, int max)
        {
            ship = new SailBoat(length, name, max, type);
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
