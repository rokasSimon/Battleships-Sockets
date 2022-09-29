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

        public override Message Execute()
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new FailResponse();

            var (myTurn, tileUpdate) = thisPlayer.JoinedSession.GetTurnFor(PlayerName);

            return new SendTileUpdateResponse(myTurn, tileUpdate);
        }
    }
}
