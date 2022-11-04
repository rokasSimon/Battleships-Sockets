using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient
{
    public class Client
    {
        Facade facade;
        public Client(Facade facade)
        {
            this.facade = facade;
        }
        public void startGame()
        {
            facade.StartGame();
        }
    }
}
