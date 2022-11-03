using BattleshipsCore.Communication;
using BattleshipsCore.Data;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class SendMapDataResponse : Message
    {
        public override MessageType Type => MessageType.SendMapData;
        public GameMapData MapData { get; set; }

        public SendMapDataResponse(GameMapData mapData)
        {
            MapData = mapData;
        }
    }
}
