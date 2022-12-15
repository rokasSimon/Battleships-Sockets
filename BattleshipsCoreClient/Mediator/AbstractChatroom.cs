using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Mediator
{
    public abstract class AbstractChatroom
    {
        public abstract void Register(AsyncSocketClient participant);
        public abstract void Send(string from, string to, string message);
    }
}
