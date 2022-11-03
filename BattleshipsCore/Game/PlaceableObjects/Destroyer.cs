using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Game.PlaceableObjects
{
    public class Destroyer : Ship
    {
        public override TileType Type => TileType.Destroyer;

        public int Length { get; set; }
        public int SideBlocks { get; set; }

        public Destroyer(string name, int length, int sideBlocks, int max) : base(name, max)
        {
            Length = length;
            SideBlocks = sideBlocks;
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

                if (i > 0 && i <= SideBlocks)
                {
                    var xSideIncrement = xIncrement;
                    var ySideIncrement = yIncrement;

                    if (xSideIncrement == 0)
                    {
                        xSideIncrement = yIncrement;
                        ySideIncrement = 0;
                    }
                    else if (ySideIncrement == 0)
                    {
                        ySideIncrement = xIncrement;
                        xSideIncrement = 0;
                    }

                    hoverTiles.Add(new Vec2(currentPosition.X + xSideIncrement, currentPosition.Y + ySideIncrement));
                    hoverTiles.Add(new Vec2(currentPosition.X - xSideIncrement, currentPosition.Y - ySideIncrement));
                }

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
