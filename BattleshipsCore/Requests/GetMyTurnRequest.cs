using BattleshipsCore.Communication;
using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class GetMyTurnRequest : Request
    {
        public override MessageType Type => MessageType.GetMyTurn;

        public string PlayerName { get; set; }

        public GetMyTurnRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var (myTurn, tileUpdate) = thisPlayer.JoinedSession.GetTurnFor(PlayerName);

            return new List<(Message, Guid)> { (new SendTileUpdateResponse(myTurn, tileUpdate), connectionId) };
        }
    }
}
