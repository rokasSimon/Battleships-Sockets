using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class InactiveTurnResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.InactiveTurn;

        public List<TileUpdate> EnemyBoardUpdates { get; set; }

        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
