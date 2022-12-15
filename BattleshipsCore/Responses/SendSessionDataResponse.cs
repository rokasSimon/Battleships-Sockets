using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

#nullable disable

namespace BattleshipsCore.Responses
{
    public class SendSessionDataResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.SendSessionData;

        public GameSessionData SessionData { get; set; }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
