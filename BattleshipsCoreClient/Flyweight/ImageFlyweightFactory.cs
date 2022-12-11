using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Flyweight
{
    public class ImageFlyweightFactory
    {
        private Dictionary<string, ImageFlyweight> flyweights { get; set; } = new Dictionary<string, ImageFlyweight>();

        public ImageFlyweightFactory()
        {
            flyweights.Add("ship", new ConcreteImageFlyweight(Image.FromFile("..\\..\\..\\Assets\\ship.png")));

        }
        public ImageFlyweight GetFlyweight(string key)
        {
            return ((ImageFlyweight)flyweights[key]);
        }
    }
}
