using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

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

        public override Message Execute()
        {
            var player = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (player == null || player.JoinedSession == null) return new FailResponse();

            var success = player.JoinedSession.StartBattle();

            if (!success) return new FailResponse();

            return new OkResponse();
        }
    }
}
