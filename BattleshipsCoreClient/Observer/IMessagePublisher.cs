using Message = BattleshipsCore.Interfaces.Message;

namespace BattleshipsCoreClient.Observer
{
    public interface IMessagePublisher
    {
        Task Notify(Message message);

        void Subscribe(ISubscriber listener);
        void Unsubscribe(ISubscriber listener);
    }
}
