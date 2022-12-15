using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class SendPlayerListResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.SendPlayerList;
        public string[] PlayerList { get; set; }

        public SendPlayerListResponse(string[] playerList)
        {
            PlayerList = playerList;
        }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
