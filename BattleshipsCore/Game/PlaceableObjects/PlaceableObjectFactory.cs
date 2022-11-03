using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public class PlaceableObjectFactory
    {
        public PlaceableObject Create(TileType type, string name, int maxCount, int length, int sideBlocks)
        {
            return type switch
            {
                TileType.InfantryTank => new InfantryTank(name, maxCount),
                TileType.CruiserTank => new CruiserTank(name, maxCount),
                TileType.AmphibiousTank => new AmphibiousTank(name, maxCount),

                TileType.Frigate => new Frigate(name, length, maxCount),
                TileType.Destroyer => new Destroyer(name, length, sideBlocks, maxCount),
                TileType.Carrier => new Carrier(name, length, sideBlocks, maxCount),

                _ => throw new ArgumentException("Unknown tile type.")
            };
        }
    }
}
