using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class StartedGameResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.StartedGame;
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
