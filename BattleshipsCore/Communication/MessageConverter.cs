using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Requests;
using BattleshipsCore.Responses;
using BattleshipsCore.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

#nullable disable

namespace BattleshipsCore.Communication
{
    internal class ConcreteMessageConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if ((typeof(Message).IsAssignableFrom(objectType) || typeof(Request).IsAssignableFrom(objectType)) && !objectType.IsAbstract) return null;

            return base.ResolveContractConverter(objectType);
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);

            contract.ItemRequired = Required.AllowNull;

            return contract;
        }
    }

    public class MessageConverter : JsonConverter
    {
        static readonly JsonSerializerSettings ResponseConverterSettings = new() { ContractResolver = new ConcreteMessageConverter() };

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

                MessageType.SendPlayerList => ToMessage<SendPlayerListResponse>(jo),
                MessageType.SendSessionKey => ToMessage<SendSessionKeyResponse>(jo),
                MessageType.SendSessionList => ToMessage<SendSessionListResponse>(jo),
                MessageType.SendSessionData => ToMessage<SendSessionDataResponse>(jo),
                MessageType.SendMapData => ToMessage<SendMapDataResponse>(jo),
                MessageType.SendTileUpdate => ToMessage<SendTileUpdateResponse>(jo),

                MessageType.GetPlayerList => ToMessage<GetPlayerListRequest>(jo),
                MessageType.GetSessionList => ToMessage<GetSessionListRequest>(jo),
                MessageType.GetSessionData => ToMessage<GetSessionDataRequest>(jo),
                MessageType.GetMapData => ToMessage<GetMapDataRequest>(jo),
                MessageType.GetOpponentMap => ToMessage<GetOpponentMapRequest>(jo),
                MessageType.GetMyTurn => ToMessage<GetMyTurnRequest>(jo),

                MessageType.JoinServer => ToMessage<JoinServerRequest>(jo),
                MessageType.JoinedServer => ToMessage<JoinedServerResponse>(jo),

                MessageType.Disconnect => ToMessage<DisconnectRequest>(jo),
                MessageType.Disconnected => ToMessage<DisconnectResponse>(jo),

                MessageType.JoinSession => ToMessage<JoinSessionRequest>(jo),
                MessageType.JoinedSession => ToMessage<JoinedSessionResponse>(jo),

                MessageType.LeaveSession => ToMessage<LeaveSessionRequest>(jo),
                MessageType.LeftSession => ToMessage<LeftSessionResponse>(jo),

                MessageType.CreateSession => ToMessage<CreateSessionRequest>(jo),

                MessageType.StartGame => ToMessage<StartGameRequest>(jo),
                MessageType.StartedGame => ToMessage<StartedGameResponse>(jo),
                MessageType.InitializeLevel => ToMessage<InitializeLevelResponse>(jo),

                MessageType.StartBattle => ToMessage<StartBattleRequest>(jo),
                MessageType.StartedBattle => ToMessage<StartedBattleResponse>(jo),

                MessageType.SetTiles => HandleSetTiles(jo),
                MessageType.UnsetTiles => ToMessage<UnsetTilesRequest>(jo),
                MessageType.Shoot => ToMessage<ShootRequest>(jo),

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
            return JsonConvert.DeserializeObject<TMessage>(val.ToString(), ResponseConverterSettings);
        }

        // Should look for a better solution, this is an exception
        private SetTilesRequest HandleSetTiles(JObject val)
        {
            var factory = new PlaceableObjectFactory();

            var playerName = val.Value<string>("PlayerName");
            var placeableObjects = new List<PlacedObject>();

            foreach (var item in val["PlacedObjects"])
            {
                var tiles = JsonConvert.DeserializeObject<List<Vec2>>(item["Tiles"].ToString());

                var objVal = item["Obj"];
                var type = (TileType)objVal.Value<int>("t");

                var name = objVal.Value<string>("Name");
                var max = objVal.Value<int>("Max");
                var length = objVal.Value<int>("Length");
                var sideBlocks = objVal.Value<int>("SideBlocks");

                var obj = factory.Create(type, name, max, length, sideBlocks);

                placeableObjects.Add(new PlacedObject(obj, tiles));
            }

            var request = new SetTilesRequest(playerName, placeableObjects);

            return request;
        }
    }
}
