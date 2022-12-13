using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class ActiveTurnResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.ActiveTurn;

        public List<TileUpdate> YourBoardUpdates { get; set; }

        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
