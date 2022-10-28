using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
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
