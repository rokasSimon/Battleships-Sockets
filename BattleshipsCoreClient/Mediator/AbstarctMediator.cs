using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Mediator
{
    abstract class AbstarctMediator
    {
            public abstract void Register(AsyncSocketClient client);
            public abstract void Send(string from, string to, string message);
    }
    
}
