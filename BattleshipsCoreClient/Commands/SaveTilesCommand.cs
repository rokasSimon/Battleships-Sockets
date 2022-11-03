using BattleshipsCore.Data;
using BattleshipsCore.Requests;

namespace BattleshipsCoreClient.Commands
{
    internal class SaveTilesCommand : ICommand
    {
        private readonly string _playerName;
        private readonly List<PlacedObject> _placedObjects;
        private readonly AsyncSocketClient _client;

        public SaveTilesCommand(AsyncSocketClient client, string playerName, List<PlacedObject> placedObjects)
        {
            _client = client;
            _playerName = playerName;
            _placedObjects = placedObjects;
        }

        public void Execute()
        {
            _client.SendMessageAsync(new SetTilesRequest(_playerName, _placedObjects))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public void Undo()
        {
            _client.SendMessageAsync(new UnsetTilesRequest(_playerName))
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }
    }
}
