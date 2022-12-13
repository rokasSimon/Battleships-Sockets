using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class SendSessionKeyResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.SendSessionKey;
        public Guid SessionKey { get; set; }

        public SendSessionKeyResponse(Guid sessionKey)
        {
            SessionKey = sessionKey;
        }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
