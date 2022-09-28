using BattleshipsCore.Interfaces;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using BattleshipsCore.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

#nullable disable

namespace BattleshipsCore.Game
{
    public class ConcreteMessageConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Message).IsAssignableFrom(objectType) && !objectType.IsAbstract) return null;

            return base.ResolveContractConverter(objectType);
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);

            contract.ItemRequired = Required.Always;

            return contract;
        }
    }

    public class ConcreteCommandConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Request).IsAssignableFrom(objectType) && !objectType.IsAbstract) return null;

            return base.ResolveContractConverter(objectType);
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);

            contract.ItemRequired = Required.Always;

            return contract;
        }
    }

    public class MessageConverter : JsonConverter
    {
        static JsonSerializerSettings ResponseConverterSettings = new() { ContractResolver = new ConcreteMessageConverter() };
        static JsonSerializerSettings RequestConverterSettings = new() { ContractResolver = new ConcreteCommandConverter() };

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Message) || objectType == typeof(Request);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            var messageCode = jo[Message.MessageTypeProperty]!.Value<int>();
            var enumType = (MessageType)messageCode;

            return enumType switch
            {
                MessageType.Ok => ToMessage<OkResponse>(jo),
                MessageType.Fail => ToMessage<FailResponse>(jo),

                MessageType.GetPlayerList => ToCommand<GetPlayerListRequest>(jo),
                MessageType.GetSessionList => ToCommand<GetSessionListRequest>(jo),
                MessageType.GetSessionData => ToCommand<GetSessionDataRequest>(jo),

                MessageType.SendPlayerList => ToMessage<SendPlayerListResponse>(jo),
                MessageType.SendSessionKey => ToMessage<SendSessionKeyResponse>(jo),
                MessageType.SendSessionList => ToMessage<SendSessionListResponse>(jo),
                MessageType.SendSessionData => ToMessage<SendSessionDataResponse>(jo),

                MessageType.JoinServer => ToCommand<JoinServerRequest>(jo),
                MessageType.Disconnect => ToCommand<DisconnectRequest>(jo),

                MessageType.JoinSession => ToCommand<JoinSessionRequest>(jo),
                MessageType.LeaveSession => ToCommand<LeaveSessionRequest>(jo),
                MessageType.CreateSession => ToCommand<CreateSessionRequest>(jo),

                _ => throw new UnknownMessageException($"Unknown message with code: {messageCode};")
            };
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }

        private TMessage ToMessage<TMessage>(JObject val) where TMessage : Message
        {
            var message = JsonConvert.DeserializeObject<TMessage>(val.ToString(), ResponseConverterSettings);

            return message;
        }

        private TCommand ToCommand<TCommand>(JObject val) where TCommand : Request
        {
            return JsonConvert.DeserializeObject<TCommand>(val.ToString(), RequestConverterSettings);
        }
    }
}
