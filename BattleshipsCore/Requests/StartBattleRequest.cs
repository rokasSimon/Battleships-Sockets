using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class StartBattleRequest : Request
    {
        public override MessageType Type => MessageType.StartBattle;

        public string PlayerName { get; set; }

        public StartBattleRequest(string playerName)
        {
            PlayerName = playerName;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var player = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (player == null || player.JoinedSession == null)
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var success = player.JoinedSession.StartBattle();

            if (!success)
                return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var players = ServerGameStateManager.Instance.GetPlayers(player.JoinedSession.PlayerNames.ToArray());

            var startedResponse = new StartedBattleResponse();
            var responses = new List<(Message, Guid)>(players.Length);

            foreach (var p in players)
            {
                responses.Add((startedResponse, p.SocketData.Id));
            }

            //foreach (var p in players)
            //{
            //    var map = p.JoinedSession!.GetOpponentMap(p.Name);

            //    if (map == null) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            //    responses.Add((new SendMapDataResponse(map), p.SocketData.Id));

            //    if (map.Active)
            //    {
            //        responses.Add((new ActiveTurnResponse { YourBoardUpdates = new List<Data.TileUpdate>() }, p.SocketData.Id));
            //    }
            //    else
            //    {
            //        responses.Add((new InactiveTurnResponse { EnemyBoardUpdates = new List<Data.TileUpdate>() }, p.SocketData.Id));
            //    }
            //}

            return responses;

            //var opponentMap = thisPlayer.JoinedSession.GetOpponentMap(PlayerName);

            //if (opponentMap == null) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            //return new List<(Message, Guid)> { (new SendMapDataResponse(opponentMap), connectionId) };
        }
    }
}
