using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class SendSessionKeyResponse : Message
    {
        public override MessageType Type => MessageType.SendSessionKey;
        public Guid SessionKey { get; set; }

        public SendSessionKeyResponse(Guid sessionKey)
        {
            SessionKey = sessionKey;
        }
    }
}
