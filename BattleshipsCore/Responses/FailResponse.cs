using BattleshipsCore.Interfaces;
using Newtonsoft.Json;

namespace BattleshipsCore.Game
{
    public class FailResponse : AcceptableResponse
    {
        public override MessageType Type => MessageType.Fail;

        [JsonProperty("e")]
        public string Explanation { get; set; }

        public FailResponse(string explanation = "")
        {
            Explanation = explanation;
        }
        public override async Task Accept(IResponseVisitor v)
        {
            await v.Visit(this);
        }
    }
}
