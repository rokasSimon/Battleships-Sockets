using BattleshipsCore.Communication;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class JoinServerRequest : Request
    {
        public override MessageType Type => MessageType.JoinServer;
        public string PlayerName { get; set; }

        public JoinServerRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var newPlayerData = new PlayerData
            {
                Name = PlayerName,
            };

            if (ServerGameStateManager.Instance.TryAddPlayer(newPlayerData))
            {
                return new List<(Message, Guid)> { (new JoinedServerResponse(), connectionId) };
            }
            else
            {
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };
            }
        }
    }
}
