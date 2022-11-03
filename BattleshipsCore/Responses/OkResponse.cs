using BattleshipsCore.Communication;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class OkResponse : Message
    {
        public override MessageType Type => MessageType.Ok;

        public string? Text { get; set; }
    }
}
