using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class LeaveSessionRequest : Request
    {
        public override MessageType Type => MessageType.LeaveSession;
        public Guid SessionToLeave { get; set; }
        public string LeavingPlayer { get; set; }

        public LeaveSessionRequest(Guid sessionToLeave, string leavingPlayer)
        {
            SessionToLeave = sessionToLeave;
            LeavingPlayer = leavingPlayer;
        }

        public override Message Execute()
        {
            if (ServerGameStateManager.Instance.TryLeavingSession(SessionToLeave, LeavingPlayer))
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
