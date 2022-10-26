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
            if (Rotation == Rotation.Left) Rotation = Rotation.Top;
            else if (Rotation == Rotation.Top) Rotation = Rotation.Right;
            else if (Rotation == Rotation.Right) Rotation = Rotation.Bottom;
            else Rotation = Rotation.Left;
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

