using BattleshipsCore.Game.PlaceableObjects.Builder;
using BattleshipsCore.Game.PlaceableObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ChainOfResponse
{
    internal class TankerHandler : Handler
    {
        public override Ship HandleType(int type, string name, int length, int max)
        {
            if (type == 6)
            {
                Console.WriteLine("Chain stopped on Tanker");
                return new Tanker(length, name, max, type);
            }
            else if (type != 0)
            {
                return shipTypeHandler.HandleType(type, name, length, max);
            }
            return null;
        }
    }
}
