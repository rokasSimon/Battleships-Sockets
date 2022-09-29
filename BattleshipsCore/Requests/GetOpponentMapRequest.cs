using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class GetOpponentMapRequest : Request
    {
        public override MessageType Type => MessageType.GetOpponentMap;
        public string PlayerName { get; set; }

        public GetOpponentMapRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override Message Execute()
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new FailResponse();

            var opponentMap = thisPlayer.JoinedSession.GetOpponentMap(PlayerName);

            if (opponentMap == null) return new FailResponse();

            return new SendMapDataResponse(opponentMap);
        }
    }
}
