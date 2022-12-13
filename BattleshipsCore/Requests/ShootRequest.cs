using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;
using BattleshipsCore.Server;

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

            try
            {
                var (isGameOver, updates) = session.Shoot(Initiator, Pos);

                var otherPlayer = session.PlayerNames.Except(new[] { thisPlayer.Name }).First();
                var otherPlayerConnection = ServerGameStateManager.Instance.GetPlayer(otherPlayer)!;

                if (isGameOver)
                {
                    return new List<(Message, Guid)>
                    {
                        (new WonGameResponse { TileUpdates = updates }, connectionId),
                        (new LostGameResponse { TileUpdates = updates }, otherPlayerConnection.SocketData.Id),
                    };
                }

                return new List<(Message, Guid)>
                {
                    (new ActiveTurnResponse { YourBoardUpdates = updates }, otherPlayerConnection.SocketData.Id),
                    (new InactiveTurnResponse { EnemyBoardUpdates = updates }, connectionId),
                };
            }
            catch (Exception e)
            {
                ServerLogger.Instance.LogError(e.Message);

                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };
            }
        }
    }
}
