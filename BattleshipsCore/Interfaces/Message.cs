using BattleshipsCore.Game;
using Newtonsoft.Json;

namespace BattleshipsCore.Interfaces
{
    [JsonConverter(typeof(MessageConverter))]
    public abstract class Message
    {
        public const string MessageTypeProperty = "ct";

        [JsonProperty(MessageTypeProperty)]
        public abstract MessageType Type { get; }
    }
}
