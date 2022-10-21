using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Requests.Guns_Requests
{
    public abstract class GunCreatorRequest : Request
    {
        public GunCreatorRequest(string initiator, Vec2 pos) 
        {
            this.createGun();
        }
        public abstract GunRequest createGun();
    }
}
