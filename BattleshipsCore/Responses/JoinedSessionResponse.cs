using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class JoinedSessionResponse : Message
    {
        public override MessageType Type => MessageType.JoinedSession;

        public Guid SessionId { get; set; }

        public JoinedSessionResponse(Guid sessionId)
        {
            SessionId = sessionId;
        }
    }
}
