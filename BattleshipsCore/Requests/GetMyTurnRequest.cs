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

            var map = thisPlayer.JoinedSession!.GetOpponentMap(thisPlayer.Name);
            if (map == null) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            Message response = map.Active
                ? new InactiveTurnResponse() { EnemyBoardUpdates = new List<Data.TileUpdate>() }
                : new ActiveTurnResponse() { YourBoardUpdates = new List<Data.TileUpdate>() };

            return new List<(Message, Guid)>() { (response, connectionId) };
        }
    }
}
