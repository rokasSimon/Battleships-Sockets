using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCore.Game.PlaceableObjects.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ChainOfResponse
{
    public abstract class Handler
    {
        protected Handler shipTypeHandler;

        protected ShipDirector director = new ShipDirector();
        public void SetShip(Handler shipTypeHandler)
        {
            this.shipTypeHandler = shipTypeHandler;            
        }
        public abstract Ship HandleType(int type, string name, int length, int max);
    }
}
