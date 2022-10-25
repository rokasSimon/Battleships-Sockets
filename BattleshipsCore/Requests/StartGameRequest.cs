using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

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

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var session = ServerGameStateManager.Instance.GetSession(SessionId);
            if (session == null)
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var success = session.StartSession();

            if (success)
            {
                var players = ServerGameStateManager.Instance.GetPlayers(session.PlayerNames.ToArray());

                var response = new StartedGameResponse();
                var responses = new List<(Message, Guid)>(players.Length);

                foreach (var p in players)
                {
                    responses.Add((response, p.SocketData.Id));
                }

                return responses;
            }
            return new List<(Message, Guid)> { (new FailResponse(), connectionId) };
        }
    }
}
