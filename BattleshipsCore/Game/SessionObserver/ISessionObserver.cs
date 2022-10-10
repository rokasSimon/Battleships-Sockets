using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.SessionObserver
{
    public interface ISessionObserver
    {
        public void Update(SessionSubject subject);
    }
}
