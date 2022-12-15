using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class StartedBattleResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.StartedBattle;
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
