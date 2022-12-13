using BattleshipsCore.Interfaces;

namespace BattleshipsCoreClient.Observer
{
    public interface ISubscriber
    {
        Task UpdateAsync(AcceptableResponse message);
    }
}
