using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class JoinedServerResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.JoinedServer;
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
