using BattleshipsCore.Game.GameGrid;
using Newtonsoft.Json;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public abstract class Tank : PlaceableObject
    {
        [JsonProperty("t")]
        public override TileType Type => TileType.Tank;

        protected Tank(string name, int max) : base(name, max)
        {

        }

        public override bool IsPlaceable(Tile[,] grid, Vec2 position)
        {
            var gridSize = new Vec2(grid.GetLength(1), grid.GetLength(0));

            var hoverTiles = HoverTiles(gridSize, position);

            if (hoverTiles.Count == 0) return false;

            foreach (var tilePosition in hoverTiles)
            {
                if (grid[tilePosition.X, tilePosition.Y].Type != TileType.Ground) return false;
            }

            return true;
        }

        public abstract bool CanMoveTo(Tile[,] grid, Vec2 start, Vec2 destination);
    }
}