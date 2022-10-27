using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class OkResponse : Message
    {
        public override MessageType Type => MessageType.Ok;

        public string? Text { get; set; }
    }
}
