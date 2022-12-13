using BattleshipsCore.Game;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Interfaces
{
    public interface IResponseVisitor
    {
        public Task Visit(ActiveTurnResponse response);
        public Task Visit(DisconnectResponse response);
        public Task Visit(FailResponse response);
        public Task Visit(InactiveTurnResponse response);
        public Task Visit(JoinedServerResponse response);
        public Task Visit(JoinedSessionResponse response);
        public Task Visit(LeftSessionResponse response);
        public Task Visit(LostGameResponse response);
        public Task Visit(NewSessionsAddedResponse response);
        public Task Visit(OkResponse response);
        public Task Visit(SendMapDataResponse response);
        public Task Visit(SendPlayerListResponse response);
        public Task Visit(SendSessionDataResponse response);
        public Task Visit(SendSessionKeyResponse response);
        public Task Visit(SendSessionListResponse response);
        public Task Visit(SendTextResponse response);
        public Task Visit(SendTileUpdateResponse response);
        public Task Visit(StartedBattleResponse response);
        public Task Visit(StartedGameResponse response);
        public Task Visit(WonGameResponse response);
    }
}
