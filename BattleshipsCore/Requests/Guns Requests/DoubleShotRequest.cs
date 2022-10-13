using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects.Guns;
using BattleshipsCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Requests.Guns_Requests
{
    public class DoubleShotRequest : GunRequest
    {
        public DoubleShotRequest(string initiator, Vec2 pos) : base(initiator, pos)
        {
        }

        public override MessageType Type => throw new NotImplementedException();

        public override Message Execute()
        {
            throw new NotImplementedException();
        }
    }
}
