using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class DisconnectResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.Disconnected;
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
