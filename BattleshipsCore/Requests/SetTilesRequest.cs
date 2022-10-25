using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Requests
{
    public class SetTilesRequest : Request
    {
        public override MessageType Type => MessageType.SetTiles;

        public string PlayerName { get; set; }
        public List<PlacedObject> PlacedObjects { get; set; }

        public SetTilesRequest(string playerName, List<PlacedObject> placedObjects)
        {
            PlayerName = playerName;
            PlacedObjects = placedObjects;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var player = ServerGameStateManager.Instance.GetPlayer(PlayerName);

            if (player == null ||
                player.JoinedSession == null ||
                !player.JoinedSession.Active) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            var success = player.JoinedSession.SetMapFor(PlayerName, PlacedObjects);

            if (!success) return new List<(Message, Guid)> { (new FailResponse(), connectionId) };

            return new List<(Message, Guid)> { (new OkResponse(), connectionId) }; ;
        }
    }
}
