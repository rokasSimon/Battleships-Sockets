using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.SessionObserver
{
    public class SessionSubject
    {
        private int _SessionCount;
        public int SessionCount
        {
            get { return this._SessionCount; }
            set { this._SessionCount = value; Notify(); }
        }

        private List<ISessionObserver> _listeners = new List<ISessionObserver>();

        public void Attach(ISessionObserver listener)
        {
            _listeners.Add(listener);
        }
        public void Detach(ISessionObserver listener)
        {
            _listeners.Remove(listener);
        }
        public void Notify()
        {
            foreach (ISessionObserver listener in _listeners)
            {
                listener.Update(this);
            }
            Console.WriteLine("session listeners notified");
        }

    }
}
