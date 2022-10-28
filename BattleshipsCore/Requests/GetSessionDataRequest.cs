using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class GetSessionDataRequest : Request
    {
        public override MessageType Type => MessageType.GetSessionData;
        public Guid SessionKey { get; set; }

        public GetSessionDataRequest(Guid sessionKey)
        {
            SessionKey = sessionKey;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var session = ServerGameStateManager.Instance.GetSession(SessionKey);

            if (session == null) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            return new List<(Message, Guid)> { (new SendSessionDataResponse
            {
                SessionData = new GameSessionData
                {
                    SessionKey = SessionKey,
                    SessionName = session.SessionName,
                    PlayerNames = session.PlayerNames,
                    Active = session.Active,
                }
            }, connectionId) };
        }
    }
}
