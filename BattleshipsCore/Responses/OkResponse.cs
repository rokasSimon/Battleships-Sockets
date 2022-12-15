using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class OkResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.Ok;

        public string? Text { get; set; }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
