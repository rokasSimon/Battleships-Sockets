using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Game
{
    public class FailResponse : Message
    {
        public override MessageType Type => MessageType.Fail;
    }
}
