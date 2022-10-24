using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class StartBattleRequest : Request
    {
        public override MessageType Type => MessageType.StartBattle;

        public string PlayerName { get; set; }

        public StartBattleRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var player = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (player == null || player.JoinedSession == null)
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var success = player.JoinedSession.StartBattle();

            if (!success)
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var players = ServerGameStateManager.Instance.GetPlayers(player.JoinedSession.PlayerNames.ToArray());

            var response = new StartedBattleResponse();
            var responses = new List<(Message, Guid)>(players.Length);

            foreach (var p in players)
            {
                responses.Add((response, p.SocketData.Id));
            }

            return responses;
        }
    }
}
