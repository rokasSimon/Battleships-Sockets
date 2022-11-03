using BattleshipsCore.Communication;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class DisconnectRequest : Request
    {
        public override MessageType Type => MessageType.Disconnect;
        public string PlayerName { get; set; }

        public DisconnectRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var player = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (player != null) ServerGameStateManager.Instance.TryRemovePlayer(player.Name);

            return new List<(Message, Guid)>() { (new DisconnectResponse(), connectionId) };
        }
    }
}
