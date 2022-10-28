using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.ShootingStrategy
{
    public class HorizontalLineShooting : ShootingStrategy
    {
        public override List<Vec2> TargetPositions(Vec2 selectedPosition)
        {
            var targetedPositions = new List<Vec2>();
            var posX = selectedPosition.X;

            for (int i = 0; i < Constants.GridRowCount; i++)
            {
                targetedPositions.Add(new Vec2(posX, i));
            }

            return targetedPositions;
        }
    }
}
