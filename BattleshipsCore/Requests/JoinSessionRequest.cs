using BattleshipsCore.Data;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

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

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var session = ServerGameStateManager.Instance.GetSession(SessionToJoin);
            var player = ServerGameStateManager.Instance.GetPlayer(JoiningPlayer);

            if (session == null || player == null) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var otherPlayerData = ServerGameStateManager.Instance.GetPlayers(session.PlayerNames.ToArray());

            var joined = session.Join(player);
            if (!joined) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var responses = new List<(Message, Guid)>(otherPlayerData.Length + 1);

            // Send update to other players
            foreach (var otherPlayer in otherPlayerData)
            {
                responses.Add((new SendSessionDataResponse
                {
                    SessionData = new GameSessionData
                    {
                        SessionKey = SessionToJoin,
                        SessionName = session.SessionName,
                        PlayerNames = session.PlayerNames,
                        Active = session.Active,
                    }
                }, otherPlayer.SocketData.Id));
            }

            // Send success to joining player
            responses.Add((new JoinedSessionResponse(SessionToJoin), connectionId));

            return responses;
        }
    }
}
