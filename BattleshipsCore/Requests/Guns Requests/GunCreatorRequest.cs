using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using System;

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
