using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.ShootingStrategy
{
    public class SingleTileShooting : ShootingStrategy
    {
        public override List<Vec2> TargetPositions(Vec2 selectedPosition)
        {
            return new List<Vec2> { selectedPosition };
        }
    }
}
