using BattleshipsCore.Data.ShipBuiilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Ship.ShipBuiilder
{
    internal class ShipDirector
    {
        public IShip Construct(ShipBuilder shipBuiilder)
        {
            return shipBuiilder.BuildShootingRange().BuildWeapons().Build();
        }
    }
}
