using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class SendMapDataResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.SendMapData;
        public GameMapData MapData { get; set; }

        public SendMapDataResponse(GameMapData mapData)
        {
            MapData = mapData;
        }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
