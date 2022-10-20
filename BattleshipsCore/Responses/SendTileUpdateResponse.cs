using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class SendTileUpdateResponse : Message
    {
        public override MessageType Type => MessageType.SendTileUpdate;

        public GameState GameState { get; set; }
        public List<TileUpdate?> TileUpdate { get; set; }

        public SendTileUpdateResponse(GameState state, List<TileUpdate?> tileUpdate)
        {
            GameState = state;
            TileUpdate = tileUpdate;
        }
    }
}
