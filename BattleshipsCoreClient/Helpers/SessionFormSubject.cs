namespace BattleshipsCoreClient.Helpers
{
    public class SessionFormSubject
    {

        private List<ISessionFormObserver> _listeners = new List<ISessionFormObserver>();

        public void Attach(ISessionFormObserver listener)
        {
            _listeners.Add(listener);
        }
        public void Detach(ISessionFormObserver listener)
        {
            _listeners.Remove(listener);
        }
        public void Notify()
        {
            foreach (ISessionFormObserver listener in _listeners)
            {
                listener.Update(this);
            }
            Console.WriteLine("session listeners notified");
        }

    }
}
