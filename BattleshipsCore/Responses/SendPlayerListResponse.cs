using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class SendPlayerListResponse : Message
    {
        public override MessageType Type => MessageType.SendPlayerList;
        public string[] PlayerList { get; set; }

        public SendPlayerListResponse(string[] playerList)
        {
            PlayerList = playerList;
        }
    }
}
