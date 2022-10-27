using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;

namespace BattleshipsCoreClient.Data
{
    public class SelectedObject
    {
        public PlaceableObject ButtonData { get; set; }
        public List<Vec2> Tiles { get; set; }

        public SelectedObject(PlaceableObject placeableObject, List<Vec2> tiles)
        {
            ButtonData = placeableObject;
            Tiles = tiles;
        }
    }
}
