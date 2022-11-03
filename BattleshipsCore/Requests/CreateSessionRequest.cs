using BattleshipsCore.Communication;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
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

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var sessionKey = ServerGameStateManager.Instance.TryCreateSession(InitiatorName, SessionName);

            if (sessionKey == null)
            {
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };
            }
            else
            {
                var players = ServerGameStateManager.Instance.GetConnectedPlayers(connectionId);
                var updateResponse = new SendSessionListResponse(ServerGameStateManager.Instance.GetSessionList());
                var responses = new List<(Message, Guid)>(players.Length);

                foreach (var player in players)
                {
                    responses.Add((updateResponse, player.SocketData!.Id));
                }

                responses.Add((new SendSessionKeyResponse(sessionKey.Value), connectionId));

                return responses;
            }
        }
    }
}
