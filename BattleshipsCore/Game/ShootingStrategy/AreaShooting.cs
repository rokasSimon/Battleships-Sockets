using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ShootingStrategy
{
    public class AreaShooting : ShootingStrategy
    {
        public override void targetPositions(List<Vec2> targetedPositions, Vec2 selectedPosition)
        {
            var posX = selectedPosition.X;
            var posY = selectedPosition.Y;

            for (var i = -1; i <= 1; i++) {
                for (var j = -1; j <= 1; j++)
                {
                    var x = posX + i;
                    var y = posY + j;
                    if (x >= 0 && x < Constants.GridColumnCount && y >= 0 && y < Constants.GridRowCount) {
                        targetedPositions.Add(new Vec2 (x, y));
                    }
                }
            }
        }
    }
}
