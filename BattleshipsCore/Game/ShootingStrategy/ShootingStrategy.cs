using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.ShootingStrategy
{
    public abstract class ShootingStrategy
    {
        public abstract List<Vec2> TargetPositions(Vec2 selectedPosition);
    }
}
