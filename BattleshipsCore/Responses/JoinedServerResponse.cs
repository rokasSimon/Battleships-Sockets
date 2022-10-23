using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class JoinedServerResponse : Message
    {
        public override MessageType Type => MessageType.JoinedServer;
    }
}
