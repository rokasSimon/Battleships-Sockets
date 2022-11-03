using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Builder
{
    public class ShipDirector
    {
        public Product_Level_One_Ships Construct(ShipBuilder shipBuiilder)
        {
            return shipBuiilder.BuildShootingRange().BuildWeapons().Build();
        }
    }
}