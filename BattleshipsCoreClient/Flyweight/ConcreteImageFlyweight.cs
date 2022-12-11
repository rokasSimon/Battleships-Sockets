using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Flyweight
{
    public class ConcreteImageFlyweight : ImageFlyweight
    {
        public ConcreteImageFlyweight(Image image) {
            Image = image;
        }

        public override Image getImage()
        {
            return Image;
        }
    }
}
