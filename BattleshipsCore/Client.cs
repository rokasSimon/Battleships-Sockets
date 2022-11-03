using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore
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
        public async static void StartGame2()
        {
            await Facade.start();

        }
    }
}
