using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class DisconnectResponse : Message
    {
        public override MessageType Type => MessageType.Disconnected;
    }
}
