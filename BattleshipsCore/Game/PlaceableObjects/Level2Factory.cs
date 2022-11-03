using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public class Level2Factory : AbstractLevelUnitFactory
    {
        public override Ship CreateShipUnit(string name, int length, int sideBlocks, int max)
        {
            if (length < 2) throw new ArgumentOutOfRangeException("Carrier must have a length of at least 2.");
            if (sideBlocks < 1) throw new ArgumentOutOfRangeException("Carrier must have at least 1 side block.");

            return new Carrier(name, length, sideBlocks, max);
        }
        public override Tank CreateTankUnit(int type, string name, int max)
        {
            if (type == (int)TileType.AmphibiousTank)
            {
                return new AmphibiousTank(name, max);
            }

            throw new ArgumentOutOfRangeException("Unknown tank type.");
        }
    }
}
