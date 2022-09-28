using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Requests
{
    public class StartGameRequest : Request
    {
        public override MessageType Type => MessageType.StartGame;

        public Guid SessionId { get; set; }

        public StartGameRequest(Guid sessionId)
        {
            SessionId = sessionId;
        }

        public override Message Execute()
        {
            var session = ServerGameStateManager.Instance.GetSession(SessionId);
            if (session == null) return new FailResponse();

            var success = session.StartSession();

            if (success) return new OkResponse();
            return new FailResponse();
        }
    }
}
