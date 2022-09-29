using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class GetMapDataRequest : Request
    {
        public override MessageType Type => MessageType.GetMapData;

        public Guid SessionId { get; set; }
        public string RequestingPlayer { get; set; }

        public GetMapDataRequest(Guid sessionId, string requestingPlayer)
        {
            SessionId = sessionId;
            RequestingPlayer = requestingPlayer;
        }

        public override Message Execute()
        {
            var session = ServerGameStateManager.Instance.GetSession(SessionId);

            if (session == null) return new FailResponse();

            var map = session.GetMapFor(RequestingPlayer);

            if (map == null) return new FailResponse();

            return new SendMapDataResponse(map);
        }
    }
}
