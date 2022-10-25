using Newtonsoft.Json.Linq;
using Message = BattleshipsCore.Interfaces.Message;

namespace BattleshipsCoreClient.Observer
{
    public class MessagePublisher
    {
        private readonly List<ISubscriber> _subscribers = new();

        public async Task Notify()
        {
            for (int i = _subscribers.Count - 1; i >= 0; i--)
            {
                await _subscribers[i].UpdateAsync();
            }
        }

        public void Subscribe(ISubscriber listener)
        {
            _subscribers.Add(listener);
        }

        public void Unsubscribe(ISubscriber listener)
        {
            _subscribers.Remove(listener);
        }
    }
}
