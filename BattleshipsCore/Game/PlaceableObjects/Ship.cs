using BattleshipsCore.Game.GameGrid;
using Newtonsoft.Json;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public abstract class Ship : PlaceableObject
    {
        [JsonProperty("t")]
        public override TileType Type => TileType.Ship;

        protected Ship(string name, int max) : base(name, max)
        {

        }

        public override bool IsPlaceable(Tile[,] grid, Vec2 position)
        {
            var gridSize = new Vec2(grid.GetLength(1), grid.GetLength(0));

            var hoverTiles = HoverTiles(gridSize, position);

            if (hoverTiles.Count == 0) return false;

            foreach (var tile in hoverTiles)
            {
                if (grid[tile.X, tile.Y].Type != TileType.Water) return false;
            }

            return true;
        }
    }
}
