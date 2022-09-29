using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Data
{
    public class TileUpdate
    {
        public Vec2 TilePosition { get; set; }
        public TileType NewType { get; set; }

        public TileUpdate(Vec2 tilePosition, TileType newType)
        {
            TilePosition = tilePosition;
            NewType = newType;
        }
    }
}
