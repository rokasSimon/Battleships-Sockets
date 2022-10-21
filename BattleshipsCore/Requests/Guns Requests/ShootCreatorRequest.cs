using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Requests.Guns_Requests
{
    public class ShootCreatorRequest : GunCreatorRequest
    {
        public override MessageType Type => MessageType.Shoot;
        public string Initiator { get; set; }
        public Vec2 Pos { get; set; }
        public ShootCreatorRequest(string initiator, Vec2 pos) : base(initiator, pos)
        {
            Initiator = initiator;
            Pos = pos;
        }

        public override GunRequest createGun()
        {
            return new ShootRequestt(Initiator, Pos);
        }

        public override Message Execute()
        {
            throw new NotImplementedException();
        }
    }
}
