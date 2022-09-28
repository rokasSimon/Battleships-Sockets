using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class GetSessionListRequest : Request
    {
        public override MessageType Type => MessageType.GetSessionList;

        public override Message Execute()
        {
            var sessionList = ServerGameStateManager.Instance.GetSessionList();

            return new SendSessionListResponse(sessionList);
        }
    }
}
