using BattleshipsCore.Game.GameGrid;
using Newtonsoft.Json;

#nullable disable

namespace BattleshipsCore.Game.PlaceableObjects
{
    public abstract class PlaceableObject
    {
        public abstract TileType Type { get; }
        public string Name { get; set; }
        public int MaximumCount { get; set; }
        public Rotation Rotation { get; set; }

        protected PlaceableObject(string name, int max)
        {
            Name = name;
            MaximumCount = max;
        }

        public void Rotate()
        {
            Rotate(90);
        }

        public void Rotate(int degrees)
        {
            var currentDegrees = (int)Rotation * 90;
            var totalRotation = (currentDegrees + degrees) % 360;

            if (totalRotation < 0)
            {
                totalRotation = 360 + totalRotation;
            }

            var nextQuarter = totalRotation / 90;

            Rotation = (Rotation)nextQuarter;
        }

        public abstract List<Vec2> HoverTiles(Vec2 gridSize, Vec2 position);
        public abstract bool IsPlaceable(Tile[,] grid, Vec2 position);
    }

    public enum Rotation
    {
        Left,
        Top,
        Right,
        Bottom,
    }
}
