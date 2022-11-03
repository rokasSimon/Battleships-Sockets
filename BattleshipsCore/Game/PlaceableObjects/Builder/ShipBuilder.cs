using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Builder
{
    public abstract class ShipBuilder
    {
        protected Product_Level_One_Ships ship;
        public Product_Level_One_Ships Ship
        {
            get { return ship; }
        }
        public abstract ShipBuilder BuildShootingRange();
        public abstract ShipBuilder BuildWeapons();

        public Product_Level_One_Ships Build()
        {
            return ship;
        }
    }
}
