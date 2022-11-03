using BattleshipsCore.Communication;
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

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var opponentMap = thisPlayer.JoinedSession.GetOpponentMap(PlayerName);

            if (opponentMap == null) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            return new List<(Message, Guid)> { (new SendMapDataResponse(opponentMap), connectionId) };
        }
    }
}
