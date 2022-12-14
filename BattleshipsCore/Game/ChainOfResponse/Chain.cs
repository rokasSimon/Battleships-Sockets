using BattleshipsCore.Game.PlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ChainOfResponse
{
    public class Chain
    {
        public static Ship ChainOfShips(int type, string name, int length, int max)
        {
            Handler boatHandler = new BoatHandler();
            Handler sailBoatHandler = new SailBoatHandler();
            Handler brigHandler = new BrigHandler();
            Handler narrowBoatHandler = new NarrowBoatHandler();
            Handler cruiseHandler = new CruiseHandler();
            Handler tankerHandler = new TankerHandler();
            boatHandler.SetShip(sailBoatHandler);
            sailBoatHandler.SetShip(brigHandler);
            brigHandler.SetShip(narrowBoatHandler);
            narrowBoatHandler.SetShip(cruiseHandler);
            cruiseHandler.SetShip(tankerHandler);

            return boatHandler.HandleType(type, name, length, max);
        }
    }
}
