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

        public override Message Execute()
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(Initiator);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new FailResponse();

            var (newGameState, tileUpdate) = thisPlayer.JoinedSession.Shoot(Initiator, Pos);

            if (newGameState == GameState.Unknown) return new FailResponse();

            return new SendTileUpdateResponse(newGameState, tileUpdate);
        }
    }
}
