using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class CreateSessionRequest : Request
    {
        public override MessageType Type => MessageType.CreateSession;
        public string InitiatorName { get; set; }
        public string SessionName { get; set; }

        public CreateSessionRequest(string initiatorName, string sessionName)
        {
            InitiatorName = initiatorName;
            SessionName = sessionName;
        }

        public override Message Execute()
        {
            var sessionKey = ServerGameStateManager.Instance.TryCreateSession(InitiatorName, SessionName);

            if (sessionKey == null)
            {
                return new FailResponse();
            }
            else
            {
                return new SendSessionKeyResponse(sessionKey.Value);
            }
        }
    }
}
