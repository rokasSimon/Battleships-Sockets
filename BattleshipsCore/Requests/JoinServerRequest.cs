using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class JoinServerRequest : Request
    {
        public override MessageType Type => MessageType.JoinServer;
        public string PlayerName { get; set; }

        public JoinServerRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override Message Execute()
        {
            var newPlayerData = new PlayerData
            {
                Name = PlayerName
            };

            if (ServerGameStateManager.Instance.TryAddPlayer(newPlayerData))
            {
                return new OkResponse();
            }
            else
            {
                return new FailResponse();
            }
        }
    }
}
