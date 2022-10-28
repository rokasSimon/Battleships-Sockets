namespace BattleshipsCoreClient.Commands
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}
