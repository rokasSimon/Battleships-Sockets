using BattleshipsCoreClient.PlacementFormComponents;

namespace BattleshipsCoreClient.Commands
{
    internal class SelectPlaceableObjectCommand : ICommand
    {
        private readonly PlaceableObjectMenu _placeableObjectMenuReceiver;

        private Guid _clickedButtonId;
        private Guid? _previousSelection;

        public SelectPlaceableObjectCommand(PlaceableObjectMenu placeableObjectMenu, Guid clickedButtonId)
        {
            _placeableObjectMenuReceiver = placeableObjectMenu;
            _clickedButtonId = clickedButtonId;
        }

        public void Execute()
        {
            _previousSelection = _placeableObjectMenuReceiver.SelectedButtonId;

            _placeableObjectMenuReceiver.SelectPlaceableObject(_clickedButtonId);
        }

        public void Undo()
        {
            _placeableObjectMenuReceiver.SelectPlaceableObject(_previousSelection);
        }
    }
}
