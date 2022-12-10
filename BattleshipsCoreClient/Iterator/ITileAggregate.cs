using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Iterator
{
    public abstract class ITileAggregate
    {
        public abstract ITileIterator CreateIterator();
    }
}
