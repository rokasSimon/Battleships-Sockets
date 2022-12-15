using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class SendTileUpdateResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.SendTileUpdate;

        public GameState GameState { get; set; }
        public List<TileUpdate> TileUpdate { get; set; }

        public SendTileUpdateResponse(GameState state, List<TileUpdate> tileUpdate)
        {
            GameState = state;
            TileUpdate = tileUpdate;
        }

        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
