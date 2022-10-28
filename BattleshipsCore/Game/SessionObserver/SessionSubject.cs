using BattleshipsCore.Server;

namespace BattleshipsCore.Game.SessionObserver
{
    public class SessionSubject
    {
        private int _sessionCount;
        public int SessionCount
        {
            get { return _sessionCount; }
            set { _sessionCount = value; Notify(); }
        }

        private List<ISessionObserver> _listeners = new();

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

            ServerLogger.Instance.LogInfo("Session listeners notified;");
        }

    }
}
