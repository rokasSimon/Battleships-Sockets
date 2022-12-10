using BattleshipsCore.Game.GameGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Iterator
{
    public interface ITileIterator
    {
        public abstract Tile First();
        public abstract Tile? Next();
        public abstract bool IsDone();
        public abstract Tile CurrentItem();
    }
}
