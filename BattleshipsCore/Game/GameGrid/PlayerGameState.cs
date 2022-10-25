using BattleshipsCore.Data;

namespace BattleshipsCore.Game.GameGrid
{
    internal class PlayerGameState
    {
        public Vec2 Size { get; init; }
        public Tile[,]? Grid { get; set; }
        public Tile[,] OriginalGrid { get; init; }

        public int TilesToHit { get; set; }
        public GameState GameState { get; set; }
        public List<TileUpdate> TileToUpdate { get; set; }

        public PlayerGameState(Vec2 size)
        {
            TilesToHit = int.MinValue;
            TileToUpdate = new List<TileUpdate>();
            GameState = GameState.EnemyTurn;
            Size = size;
            OriginalGrid = new Tile[size.X, size.Y];
        }
    }
}
