using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public class AmphibiousTank : Tank
    {
        public override TileType Type => TileType.AmphibiousTank;



        public AmphibiousTank(string name, int max) : base(name, max)
        {

        }

        public override bool CanMoveTo(Tile[,] grid, Vec2 start, Vec2 destination)
        {
            if (!IsPlaceable(grid, destination) ||
                start.Equals(destination) ||
                (int)start.Distance(destination) > 1) return false;

            return true;
        }

        public override List<Vec2> HoverTiles(Vec2 gridSize, Vec2 position)
        {
            if (position.IsInsideGrid(gridSize))
            {
                return new List<Vec2> { position };
            }

            return new List<Vec2>();
        }

        public override bool IsPlaceable(Tile[,] grid, Vec2 position)
        {
            var gridSize = new Vec2(grid.GetLength(1), grid.GetLength(0));

            var hoverTiles = HoverTiles(gridSize, position);

            if (hoverTiles.Count == 0) return false;

            foreach (var tilePosition in hoverTiles)
            {
                if (grid[tilePosition.X, tilePosition.Y].Type != TileType.Ground &&
                    grid[tilePosition.X, tilePosition.Y].Type != TileType.Water) return false;
            }

            return true;
        }
    }
}
