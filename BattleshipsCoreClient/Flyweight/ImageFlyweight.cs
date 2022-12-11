using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Flyweight
{
    public abstract class ImageFlyweight
    {
        protected Image Image;

        public abstract Image getImage();
    }
}
