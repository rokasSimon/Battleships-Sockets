using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Requests
{
    public class UnsetTilesRequest : Request
    {
        public override MessageType Type => MessageType.UnsetTiles;

        public string PlayerName { get; set; }

        public UnsetTilesRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var player = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (player == null ||
                player.JoinedSession == null ||
                !player.JoinedSession.Active) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var success = player.JoinedSession.UnsetMapFor(PlayerName);

            if (!success) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            return new List<(Message, Guid)> { (new OkResponse { Text = "Tiles were unset successfully!" }, connectionId) };
        }
    }
}
