using BattleshipsCore.Communication;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class GetSessionListRequest : Request
    {
        public override MessageType Type => MessageType.GetSessionList;

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var sessionList = ServerGameStateManager.Instance.GetSessionList();

            return new List<(Message, Guid)> { (new SendSessionListResponse(sessionList), connectionId) };
        }
    }
}
