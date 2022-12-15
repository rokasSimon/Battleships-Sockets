using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class JoinedSessionResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.JoinedSession;

        public Guid SessionId { get; set; }

        public JoinedSessionResponse(Guid sessionId)
        {
            SessionId = sessionId;
        }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
