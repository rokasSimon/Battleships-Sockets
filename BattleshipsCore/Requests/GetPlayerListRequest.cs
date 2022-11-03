using BattleshipsCore.Communication;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;

namespace BattleshipsCore.Requests
{
    public class GetPlayerListRequest : Request
    {
        public override MessageType Type => MessageType.GetPlayerList;
        public string ExcludedPlayerName { get; set; }

        public GetPlayerListRequest(string excludedPlayerName)
        {
            ExcludedPlayerName = excludedPlayerName;
        }

        public override List<(Message, Guid)> Execute(Guid connectionId)
        {
            var enemies = ServerGameStateManager.Instance.GetAvailablePlayers(ExcludedPlayerName);

            return new List<(Message, Guid)> { (new SendPlayerListResponse(enemies), connectionId) };
        }
    }
}
