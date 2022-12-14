using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCore.Game.PlaceableObjects.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ChainOfResponse
{
    public class BrigHandler : Handler
    {
        public override Ship HandleType(int type, string name, int length, int max)
        {
            if (type == 3)
            {
                Console.WriteLine("Chain stopped on Brig");
                return director.Construct(new ThreeSailShipBuilder(length, name, max, type));
            }
            else if (type != 0)
            {
                return shipTypeHandler.HandleType(type, name, length, max);
            }
            return null;
        }
    }
}
