using BattleshipsCore.Data;
using BattleshipsCore.Data.Surface;
using BattleshipsCore.Server;

namespace BattleshipsCore.Game.GameGrid
{
    internal class PlayerGameState
    {
        private DropoutStack<Tile[,]> _setMaps;

        public Vec2 Size { get; init; }
        public Tile[,]? Grid
        {
            get => _setMaps.Peek();
            set => _setMaps.Push(value);
        }
        public Tile[,] OriginalGrid { get; init; }

        public int TilesToHit { get; set; }
        public GameState GameState { get; set; }
        public List<TileUpdate> TileToUpdate { get; set; }

        public PlayerGameState(Vec2 size)
        {
            _setMaps = new DropoutStack<Tile[,]>(3);

            TilesToHit = int.MinValue;
            TileToUpdate = new List<TileUpdate>();
            GameState = GameState.EnemyTurn;
            Size = size;
            //OriginalGrid = new Tile[size.X, size.Y];
            OriginalGrid = initialize(size);
        }
        private Tile[,] initialize(Vec2 siz)
        {
            Tile[,] arr = new Tile[siz.X, siz.Y];
            arr = new GrassCreator().FactoryMethod(siz, arr);
            arr = new GroundCreator().FactoryMethod(siz, arr);
            arr = new WaterCreator().FactoryMethod(siz, arr);

            return arr;
        }

        public bool UnsetMap()
        {
            var removedTiles = _setMaps.Pop();

            return removedTiles != null;
        }

        public bool UnsetMap()
        {
            var removedTiles = _setMaps.Pop();

            return removedTiles != null;
        }
    }
}
