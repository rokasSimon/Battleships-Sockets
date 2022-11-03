using BattleshipsCore.Communication;
using BattleshipsCore.Interfaces;
using Newtonsoft.Json;

namespace BattleshipsCore.Responses
{
    public class FailResponse : Message
    {
        public override MessageType Type => MessageType.Fail;

        [JsonProperty("e")]
        public string Explanation { get; set; }

        public FailResponse(string explanation = "")
        {
            Explanation = explanation;
        }
    }
}
