using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ShootingStrategy
{
    public class VerticalLineShooting : ShootingStrategy
    {
        public override void targetPositions(List<Vec2> targetedPositions, Vec2 selectedPosition)
        {
            var posY = selectedPosition.Y;

            for (int i = 0; i < Constants.GridColumnCount; i++)
            {

                targetedPositions.Add(new Vec2(i, posY));
            }
        }
    }
}
