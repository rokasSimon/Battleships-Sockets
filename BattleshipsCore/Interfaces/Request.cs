using BattleshipsCore.Game;
using Newtonsoft.Json;

namespace BattleshipsCore.Interfaces
{
    [JsonConverter(typeof(MessageConverter))]
    public abstract class Request : Message
    {
        public abstract List<(Message, Guid)> Execute(Guid connectionId);
    }
}
