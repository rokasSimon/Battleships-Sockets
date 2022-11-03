using BattleshipsCore.Communication;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class DisconnectResponse : Message
    {
        public override MessageType Type => MessageType.Disconnected;
    }
}
