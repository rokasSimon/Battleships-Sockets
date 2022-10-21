using BattleshipsCore.Game.SessionObserver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Helpers
{
    public interface ISessionFormObserver
    {
        public void Update(SessionFormSubject subject);
    }
}
