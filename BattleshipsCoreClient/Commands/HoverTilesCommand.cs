using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;
using BattleshipsCoreClient.PlacementFormComponents;

namespace BattleshipsCoreClient.Commands
{
    internal class HoverTilesCommand : ICommand
    {
        private readonly PlaceableObject _placeableObject;
        private readonly TileGrid _tileGrid;
        private Vec2 _position;

        public HoverTilesCommand(Vec2 position, PlaceableObject placeableObject, TileGrid tileGrid)
        {
            _position = position;
            _placeableObject = placeableObject;
            _tileGrid = tileGrid;
        }

        public void Execute()
        {
            var hoverTiles = _placeableObject.HoverTiles(_tileGrid.GridSize, _position);

            _tileGrid.ColorTiles(hoverTiles, Color.LightGray);
        }

        public void Undo()
        {
            var hoverTiles = _placeableObject.HoverTiles(_tileGrid.GridSize, _position);

            _tileGrid.RestoreTileColor(hoverTiles);
        }
    }
}
