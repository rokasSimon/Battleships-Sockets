using BattleshipsCore.Interfaces;
using Newtonsoft.Json;
using System.Xml.Serialization;

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

        public string SerializeMessage(Message responseCommand, bool useXml = false)
        {
            if (useXml)
            {
                var json = JsonConvert.SerializeObject(responseCommand, Formatting.None);

                var xml = JsonConvert.DeserializeXNode(json, "root");

                var rawXml = xml!.ToString();

                if (!rawXml.StartsWith("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"))
                {
                    rawXml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + rawXml;
                }

                return rawXml;
            }
            else
            {
                return JsonConvert.SerializeObject(responseCommand, Formatting.None);
            }
        }
    }
}
