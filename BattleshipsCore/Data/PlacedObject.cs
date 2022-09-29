using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects;

namespace BattleshipsCore.Data
{
    public class PlacedObject
    {
        public PlaceableObject Obj { get; set; }
        public List<Vec2> Tiles { get; set; }

        public PlacedObject(PlaceableObject obj, List<Vec2> tiles)
        {
            Obj = obj;
            Tiles = tiles;
        }
    }
}
