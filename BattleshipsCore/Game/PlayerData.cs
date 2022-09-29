using BattleshipsCore.Server;

namespace BattleshipsCore.Game
{
    internal class PlayerData
    {
        public string Name { get; set; }
        public SocketStateData? SocketData { get; set; }
        public GameSession? JoinedSession { get; set; }
    }
}
