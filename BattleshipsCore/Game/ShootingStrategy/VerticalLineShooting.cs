using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.ShootingStrategy
{
    public class VerticalLineShooting : ShootingStrategy
    {
        public override List<Vec2> TargetPositions(Vec2 selectedPosition)
        {
            var targetedPositions = new List<Vec2>();
            var posY = selectedPosition.Y;

            for (int i = 0; i < Constants.GridColumnCount; i++)
            {
                targetedPositions.Add(new Vec2(i, posY));
            }

            return targetedPositions;
        }
    }
}
