using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class ShootRequest : Request
    {
        public override MessageType Type => MessageType.Shoot;

        public string Initiator { get; set; }
        public List<Vec2> Pos { get; set; }

        public ShootRequest(string initiator, List<Vec2> pos)
        {
            Initiator = initiator;
            Pos = pos;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(Initiator);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive)
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var session = thisPlayer.JoinedSession;

            var (newGameState, tileUpdate) = session.Shoot(Initiator, Pos);

            if (newGameState == GameState.Unknown)
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var otherPlayer = session.PlayerNames.Except(new[] { thisPlayer.Name }).First();
            var otherPlayerData = ServerGameStateManager.Instance.GetPlayer(otherPlayer)!;
            var (otherPlayerState, otherPlayerUpdates) = session.GetTurnFor(otherPlayer);

            return new List<(Message, Guid)>
            { 
                (new SendTileUpdateResponse(newGameState, tileUpdate), connectionId),
                (new SendTileUpdateResponse(otherPlayerState, otherPlayerUpdates), otherPlayerData.SocketData.Id),
            };
        }
    }
}
