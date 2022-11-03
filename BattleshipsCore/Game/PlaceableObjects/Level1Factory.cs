using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public class Level1Factory : AbstractLevelUnitFactory
    {
        public override Ship CreateShipUnit(string name, int length, int sideBlocks, int max)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException("Ship has to have some length");
            }

            if (sideBlocks == 0)
            {
                return new Frigate(name, length, max);
            }
            else
            {
                return new Destroyer(name, length, sideBlocks, max);
            }
        }

        public override Tank CreateTankUnit(int type, string name, int max)
        {
            if (type == (int)TileType.InfantryTank)
            {
                return new InfantryTank(name, 2, max);
            }
            else if (type == (int)TileType.CruiserTank)
            {
                return new CruiserTank(name, 2, max);
            }
            else if (type == (int)TileType.AmphibiousTank)
            {
                throw new ArgumentOutOfRangeException("Too low level to use amphibious tanks.");
            }

            throw new ArgumentOutOfRangeException("Unknown tank type.");
        }
    }
}
