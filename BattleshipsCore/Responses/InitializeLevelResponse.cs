using BattleshipsCore.Communication;
using BattleshipsCore.Data;
using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class InitializeLevelResponse : Message
    {
        public override MessageType Type => MessageType.InitializeLevel;

        public int Level { get; set; }
        public List<AllowedUnitData> AllowedObjects { get; set; }

        public InitializeLevelResponse(int level, List<AllowedUnitData> allowedObjects)
        {
            Level = level;
            AllowedObjects = allowedObjects;
        }
    }
}
