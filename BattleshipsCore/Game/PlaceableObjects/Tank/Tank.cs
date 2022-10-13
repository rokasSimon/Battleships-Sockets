using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Game.PlaceableObjects.Ship;
using Newtonsoft.Json;


namespace BattleshipsCore.Game.PlaceableObjects.Tank
{
    
    public class Tank : PlaceableObject
    {
        public Tank(string name, int max) : base(name, max)
        {
        }

        [JsonProperty("t")]
        public override TileType Type => TileType.Tank; // tank = 7

        public int Length { get; init; }

        public Tank (string name, int max, int length) : base(name, max)
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

        public override IShip GetShip(string name)
        {

            if (name == "small ship")
            {
                return new SmallShip("small ship", 3, 1);
            }
            if (name == "medium ship")
            {
                return new MediumShip("medium ship", 2, 2);
            }
            if (name == "large ship")
            {
                return new LargeShip("large ship", 1, 3);
            }
            else
                return null;
        }

        public override ITank GetTank(string name)
        {
            if (name == "small tank")
            {
                return new SmallTank("small tank", 3, 1);
            }
            if (name == "medium tank")
            {
                return new MediumTank("medium tank", 2, 2);
            }
            if (name == "large tank")
            {
                return new LargeTank("large tank", 1, 3);
            }
            else
                return null;
        }
    }
}
