using Message = BattleshipsCore.Interfaces.Message;

namespace BattleshipsCoreClient.Observer
{
    public interface ISubscriber
    {
        Task UpdateAsync(Message message);
    }
}
