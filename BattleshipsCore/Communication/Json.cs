using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BattleshipsCore.Communication
{
    public class Json : ParseableMessage
    {
        public Json(string data) : base(data)
        {
        }

        public override bool IsParseable()
        {
            if (string.IsNullOrWhiteSpace(Data)) return false;

            Data = Data.Trim();

            if ((Data.StartsWith('{') && Data.EndsWith('}')) ||
                (Data.StartsWith('[') && Data.EndsWith(']')))
            {
                try
                {
                    var obj = JObject.Parse(Data);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override TMessage Parse<TMessage>()
        {
            try
            {
                var result = JsonConvert.DeserializeObject<TMessage>(Data);

                if (result == null) return default!;

                return result;
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
