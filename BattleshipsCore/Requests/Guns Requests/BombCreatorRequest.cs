using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests.Guns_Requests
{
    public class BombCreatorRequest : GunCreatorRequest
    {
        public override MessageType Type => MessageType.Bomb;

        public string Initiator { get; set; }
        public Vec2 Pos { get; set; }

        public BombCreatorRequest(string initiator, Vec2 pos) : base(initiator, pos)
        {
            Initiator = initiator;
            Pos = pos;
        }

        
        public override GunRequest createGun()
        {
            return new BombRequest(Initiator,Pos);
        }

        public override Message Execute()
        {
            throw new NotImplementedException();
        }
    }
}
