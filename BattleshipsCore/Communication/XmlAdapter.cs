using Newtonsoft.Json;
using System.Xml;

namespace BattleshipsCore.Communication
{
    public class XmlAdapter : ParseableMessage
    {
        public XmlAdapter(string data) : base(data)
        {
        }

        public override bool IsParseable()
        {
            if (!Data.StartsWith("<?xml version=\"1.0\" encoding=\"UTF-8\"?>")) return false;

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Data);

                // Should only have xml version/encoding header node and class root node
                if (xmlDoc.ChildNodes.Count != 2) return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override TMessage Parse<TMessage>()
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(Data);

                var rawJson = JsonConvert.SerializeXmlNode(xmlDoc.ChildNodes[1], Newtonsoft.Json.Formatting.None, true);
                var json = new Json(rawJson);

                return json.Parse<TMessage>();
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
