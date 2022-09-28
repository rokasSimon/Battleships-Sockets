using BattleshipsCore.Interfaces;
using Newtonsoft.Json;

namespace BattleshipsCore.Game
{
    public class GameMessageParser : IMessageParser
    {
        public TRequest ParseRequest<TRequest>(string message) where TRequest : Request
        {
            return JsonConvert.DeserializeObject<TRequest>(message)!;
        }

        public TMessage ParseResponse<TMessage>(string message) where TMessage : Message
        {
            return JsonConvert.DeserializeObject<TMessage>(message)!;
        }

        public string SerializeMessage(Message responseCommand)
        {
            return JsonConvert.SerializeObject(responseCommand, Formatting.None);
        }
    }
}
