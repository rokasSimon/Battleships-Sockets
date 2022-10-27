using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCoreClient.Data;
using BattleshipsCoreClient.PlacementFormComponents;

namespace BattleshipsCoreClient.Commands
{
    internal class PlaceObjectCommand : ICommand
    {
        private readonly Vec2 _clickedPosition;
        private readonly Guid _selectedObjectId;
        private readonly PlaceableObject _selectedObject;
        private readonly List<PlacedObject> _selectedTileGroups;
        private readonly TileGrid _buttonGrid;
        private readonly PlaceableObjectMenu _placeableObjectMenu;

        private Guid? _previouslySelectedObjectId;
        private List<TileUpdate>? _previouslySelectedTileData;
        private int? _previouslyAddedTileGroupIndex;

        public PlaceObjectCommand(
            Vec2 clickedPosition,
            Guid selectedObjectId,
            PlaceableObject selectedObject,
            List<PlacedObject> selectedTileGroups,
            TileGrid buttonGrid,
            PlaceableObjectMenu placeableObjectMenu)
        {
            _clickedPosition = clickedPosition;
            _selectedObjectId = selectedObjectId;
            _selectedObject = selectedObject;
            _selectedTileGroups = selectedTileGroups;
            _buttonGrid = buttonGrid;
            _placeableObjectMenu = placeableObjectMenu;
        }

        public void Execute()
        {
            var tiles = _selectedObject.HoverTiles(_buttonGrid.GridSize, _clickedPosition);

            _previouslySelectedObjectId = _selectedObjectId;
            _previouslySelectedTileData = _buttonGrid.SetTiles(tiles, _selectedObject.Type);
            _previouslyAddedTileGroupIndex = _selectedTileGroups.Count;

            _selectedTileGroups.Add(new PlacedObject(_selectedObject, tiles));

            _placeableObjectMenu.UpdateSelection(_selectedObjectId, true);
        }

        public void Undo()
        {
            _previouslySelectedTileData = _buttonGrid.SetTiles(_previouslySelectedTileData!);
            _selectedTileGroups.RemoveAt(_previouslyAddedTileGroupIndex!.Value);
            _placeableObjectMenu.UpdateSelection(_previouslySelectedObjectId!.Value, false);
        }
    }
}
