using BattleshipsCore.Communication;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class StartedBattleResponse : Message
    {
        public override MessageType Type => MessageType.StartedBattle;
    }
}
