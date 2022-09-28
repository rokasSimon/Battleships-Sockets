using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class JoinSessionRequest : Request
    {
        public override MessageType Type => MessageType.JoinSession;
        public Guid SessionToJoin { get; set; }
        public string JoiningPlayer { get; set; }

        public JoinSessionRequest(Guid sessionToJoin,  string joiningPlayer)
        {
            SessionToJoin = sessionToJoin;
            JoiningPlayer = joiningPlayer;
        }

        public override Message Execute()
        {
            if (ServerGameStateManager.Instance.TryJoiningSession(SessionToJoin, JoiningPlayer))
            {
                return new OkResponse();
            }
            else
            {
                return new FailResponse();
            }
        }
    }
}
