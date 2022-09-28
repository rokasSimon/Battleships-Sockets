using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class GetPlayerListRequest : Request
    {
        public override MessageType Type => MessageType.GetPlayerList;
        public string ExcludedPlayerName { get; set; }

        public GetPlayerListRequest(string excludedPlayerName)
        {
            ExcludedPlayerName = excludedPlayerName;
        }

        public override Message Execute()
        {
            var enemies = ServerGameStateManager.Instance.GetAvailablePlayers(ExcludedPlayerName);

            return new SendPlayerListResponse(enemies);
        }
    }
}
