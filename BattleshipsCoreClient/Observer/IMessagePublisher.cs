using BattleshipsCore.Interfaces;

namespace BattleshipsCoreClient.Observer
{
    public interface IMessagePublisher
    {
        Task Notify(AcceptableResponse message);

        void Subscribe(ISubscriber listener);
        void Unsubscribe(ISubscriber listener);
    }
}
