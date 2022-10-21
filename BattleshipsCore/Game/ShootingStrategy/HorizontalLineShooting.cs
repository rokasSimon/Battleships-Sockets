using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ShootingStrategy
{
    public class HorizontalLineShooting : ShootingStrategy
    {
        public override void targetPositions(List<Vec2> targetedPositions, Vec2 selectedPosition)
        {
            var posX = selectedPosition.X;

            for (int i = 0; i < Constants.GridRowCount; i++)
            {

                targetedPositions.Add(new Vec2(posX, i));
            }
        }
    }
}
