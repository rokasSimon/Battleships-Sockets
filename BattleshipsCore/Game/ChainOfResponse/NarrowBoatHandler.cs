using BattleshipsCore.Game.PlaceableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ChainOfResponse
{
    public class NarrowBoatHandler : Handler
    {
        public override Ship HandleType(int type, string name, int length, int max)
        {
            if (type == 4)
            {
                Console.WriteLine("Chain stopped on Narrow Boat");
                return new NarrowBoat(length, name, max, type);
            }
            else if (type != 0)
            {
                return shipTypeHandler.HandleType(type, name, length, max);
            }
            return null;
        }
    }
}
