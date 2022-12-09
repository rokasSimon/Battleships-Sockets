using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class SendTextResponse : Message
    {
        public override MessageType Type => MessageType.SendText;
        public string Text { get; set; }

        public SendTextResponse(string text)
        {
            Text = text;
        }
    }
}
