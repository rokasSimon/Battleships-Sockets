using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class StartedGameResponse : Message
    {
        public override MessageType Type => MessageType.StartedGame;
    }
}
