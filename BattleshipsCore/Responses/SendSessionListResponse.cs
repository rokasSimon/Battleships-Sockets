using BattleshipsCore.Data;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class SendSessionListResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.SendSessionList;
        public List<GameSessionData> SessionList { get; set; }

        public SendSessionListResponse(List<GameSessionData> sessionList)
        {
            SessionList = sessionList;
        }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
