using BattleshipsCore.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game
{
    internal class PlayerData
    {
        public string Name { get; set; }
        public SocketStateData? SocketData { get; set; }
        public GameSession? JoinedSession { get; set; }
    }
}
