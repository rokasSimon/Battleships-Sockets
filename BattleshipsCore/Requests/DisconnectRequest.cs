using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class DisconnectRequest : Request
    {
        public override MessageType Type => MessageType.Disconnect;
        public string PlayerName { get; set; }

        public DisconnectRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override Message Execute()
        {
            var player = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (player != null) ServerGameStateManager.Instance.TryRemovePlayer(player.Name);

            return new OkResponse();
        }
    }
}
