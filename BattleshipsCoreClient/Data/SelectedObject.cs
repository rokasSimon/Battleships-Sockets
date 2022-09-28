using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCoreClient.Data
{
    public class SelectedObject
    {
        public PlaceableObjectButton ButtonData { get; set; }
        public List<Vec2> Tiles { get; set; }

        public SelectedObject(PlaceableObjectButton buttonData, List<Vec2> tiles)
        {
            ButtonData = buttonData;
            Tiles = tiles;
        }
    }
}
