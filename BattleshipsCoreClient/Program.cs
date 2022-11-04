#nullable disable

using BattleshipsCore.Game;
using BattleshipsCore.Requests;
using BattleshipsCoreClient.Observer;
using System.Windows.Forms;

namespace BattleshipsCoreClient
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Client client = new Client(new Facade());
            client.startGame();
        }
    }
}