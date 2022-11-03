using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public class Frigate : Ship
    {
        public override TileType Type => TileType.Frigate;

        public int Length { get; set; }

        public Frigate(string name, int length, int max) : base(name, max)
        {
            Length = length;
        }

        public override List<Vec2> HoverTiles(Vec2 gridSize, Vec2 position)
        {
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
            var hoverTiles = new List<Vec2>();

            for (int i = 0; i < Length; i++)
            {
                hoverTiles.Add(currentPosition);

                currentPosition = new Vec2(currentPosition.X + xIncrement, currentPosition.Y + yIncrement);
            }

            foreach (var tilePosition in hoverTiles)
            {
                if (!tilePosition.IsInsideGrid(gridSize))
                {
                    return new List<Vec2>();
                }
            }

            return hoverTiles;
        }
    }
}
