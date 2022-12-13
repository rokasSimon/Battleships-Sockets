using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class LeftSessionResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.LeftSession;

        public Guid SessionKey { get; set; }

        public LeftSessionResponse(Guid sessionKey)
        {
            SessionKey = sessionKey;
        }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
