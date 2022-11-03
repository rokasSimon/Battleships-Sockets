using BattleshipsCore.Communication;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class LeftSessionResponse : Message
    {
        public override MessageType Type => MessageType.LeftSession;

        public Guid SessionKey { get; set; }

        public LeftSessionResponse(Guid sessionKey)
        {
            SessionKey = sessionKey;
        }
    }
}
