using BattleshipsCore.Data;
using BattleshipsCore.Game.SessionObserver;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;
using BattleshipsCore.Server;
using System.Text;

namespace BattleshipsCore.Game
{
    internal class PlayerData : ISessionObserver
    {
        public string Name { get; set; }
        public SocketStateData? SocketData { get; set; }
        public GameSession? JoinedSession { get; set; }

        public void Update(SessionSubject subject)
        {
            if (SocketData == null) return;

            var sessionCount = ServerGameStateManager.Instance.SessionCount;
            var message = new NewSessionsAddedResponse(sessionCount);
            var commandMessage = new GameMessageParser().SerializeMessage(message);

            ServerLogger.Instance.LogInfo("New sessions (session count: " + sessionCount + ") were added, sending to: \"" + Name + "\"");

            SocketData.Socket.Send(Encoding.UTF8.GetBytes(commandMessage));
        }
    }
}
