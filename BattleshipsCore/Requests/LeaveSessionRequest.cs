using BattleshipsCore.Communication;
using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
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

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var session = ServerGameStateManager.Instance.GetSession(SessionToLeave);
            var player = ServerGameStateManager.Instance.GetPlayer(LeavingPlayer);

            if (session == null || player == null) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var otherPlayerData = ServerGameStateManager.Instance.GetPlayers(session.PlayerNames.ToArray());

            var left = session.Leave(player);
            if (!left) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            if (session.PlayerNames.Count == 0) ServerGameStateManager.Instance.RemoveSession(SessionToLeave);

            var responses = new List<(Message, Guid)>(otherPlayerData.Length + 1);

            // Send update to other players
            foreach (var otherPlayer in otherPlayerData)
            {
                responses.Add((new SendSessionDataResponse
                {
                    SessionData = new GameSessionData
                    {
                        SessionKey = SessionToLeave,
                        SessionName = session.SessionName,
                        PlayerNames = session.PlayerNames,
                        Active = session.Active,
                    }
                }, otherPlayer.SocketData.Id));
            }

            // Send success to leaving player
            responses.Add((new LeftSessionResponse(SessionToLeave), connectionId));

            return responses;
        }
    }
}
