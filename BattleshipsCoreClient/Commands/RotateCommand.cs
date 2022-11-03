using BattleshipsCore.Game.PlaceableObjects;

namespace BattleshipsCoreClient.Commands
{
    internal class RotateCommand : ICommand
    {
        private readonly PlaceableObject _selectedPlaceableObject;

        public RotateCommand(PlaceableObject selectedPlaceableObject)
        {
            _selectedPlaceableObject = selectedPlaceableObject;
        }

        public void Execute()
        {
            _selectedPlaceableObject.Rotate(90);
        }

        public void Undo()
        {
            _selectedPlaceableObject.Rotate(-90);
        }
    }
}
