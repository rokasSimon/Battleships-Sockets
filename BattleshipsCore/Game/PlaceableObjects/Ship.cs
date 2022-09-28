using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public class Ship : PlaceableObject
    {
        public override TileType Type => TileType.Ship;

        public int Length { get; init; }

        public Ship(string name, int max, int length) : base(name, max)
        {
            Length = length;
        }

        public override List<Vec2> HoverTiles(Vec2 gridSize, Vec2 position)
        {
            var hoverTiles = new List<Vec2>();

            int xIncrement = 0;
            int yIncrement = 0;

            switch (Rotation)
            {
                case Rotation.Left: xIncrement = -1; break;
                case Rotation.Right: xIncrement = +1; break;
                case Rotation.Top: yIncrement = +1; break;
                case Rotation.Bottom: yIncrement = -1; break;
                default: throw new ArgumentException("Unknown direction");
            }

            var currentPosition = position;

            for (int i = 0; i < Length; i++)
            {
                if (TileIsInsideGrid(gridSize, currentPosition)) hoverTiles.Add(currentPosition);

                currentPosition = new Vec2(currentPosition.X + xIncrement, currentPosition.Y + yIncrement);
            }

            return hoverTiles;
        }

        public override bool IsPlaceable(Tile[,] grid, Vec2 position)
        {
            var gridSize = new Vec2(grid.GetLength(1), grid.GetLength(0));

            if (!TileIsInsideGrid(gridSize, position)) return false;

            int xIncrement = 0;
            int yIncrement = 0;

            switch (Rotation)
            {
                case Rotation.Left: xIncrement = -1; break;
                case Rotation.Right: xIncrement = +1; break;
                case Rotation.Top: yIncrement = +1; break;
                case Rotation.Bottom: yIncrement = -1; break;
                default: throw new ArgumentException("Unknown direction");
            }

            var currentPosition = position;

            for (int i = 0; i < Length; i++)
            {
                if (!TileIsInsideGrid(gridSize, currentPosition) || grid[currentPosition.X, currentPosition.Y].Type != TileType.Water) return false;

                currentPosition = new Vec2(currentPosition.X + xIncrement, currentPosition.Y + yIncrement);
            }

            return true;
        }

        private static bool TileIsInsideGrid(Vec2 gridSize, Vec2 tilePosition)
        {
            if (tilePosition.X < 0
                || tilePosition.Y < 0
                || tilePosition.X >= gridSize.X
                || tilePosition.Y >= gridSize.Y)
                return false;

            return true;
        }
    }
}
