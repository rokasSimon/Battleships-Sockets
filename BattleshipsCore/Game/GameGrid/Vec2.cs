namespace BattleshipsCore.Game.GameGrid
{
    public struct Vec2
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vec2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }

        public static bool InsideGrid(Vec2 position, Vec2 gridSize)
        {
            if (position.X < 0
                || position.Y < 0
                || position.X >= gridSize.X
                || position.Y >= gridSize.Y)
                return false;

            return true;
        }
    }
}
