using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class WonGameResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.WonGame;

        public List<TileUpdate> TileUpdates { get; set; }

        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
