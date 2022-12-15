using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class SendTextResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.SendText;
        public string Text { get; set; }

        public SendTextResponse(string text)
        {
            Text = text;
        }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
