using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ShootingStrategy
{
    public class SingleTileShooting : ShootingStrategy
    {
        public override void targetPositions(List<Vec2> targetedPositions, Vec2 selectedPosition)
        {
            targetedPositions.Add(selectedPosition);
        }
    }
}
