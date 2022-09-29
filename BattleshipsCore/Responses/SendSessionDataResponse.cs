using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

#nullable disable

namespace BattleshipsCore.Responses
{
    public class SendSessionDataResponse : Message
    {
        public override MessageType Type => MessageType.SendSessionData;

        public GameSessionData SessionData { get; set; }
    }
}
