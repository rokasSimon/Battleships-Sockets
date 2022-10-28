using BattleshipsCore.Data;
using BattleshipsCore.Data.Surface;

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
            OriginalGrid = initialize(size);
        }

        private Tile[,] initialize (Vec2 siz)
        {
            Tile[,] arr = new Tile[siz.X,siz.Y];
            arr = new GrassCreator().FactoryMethod(siz, arr);
            arr = new GroundCreator().FactoryMethod(siz, arr);
            arr = new WaterCreator().FactoryMethod(siz, arr);


            

            return arr;
        }

        char[,] array2D = new char[,] { { 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'g', 'w', 'w', 'w', 'w', 'w', 'w', 'w' },
                                        { 'w', 'w', 'w', 'w', 'g', 'g', 'g', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w' },
                                        { 'w', 'w', 'w', 'g', 'g', 'g', 'g', 'w', 'g', 'w', 'w', 'w', 'w', 'w', 'w' },
                                        { 'w', 'w', 'w', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'w', 'w', 'g', 'g', 'z', 'z', 'z', 'z', 'g', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'w', 'g', 'g', 'g', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'w', 'g', 'g', 'g', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'g', 'g', 'w', 'g', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'g', 'w', 'w', 'g', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'w', 'w', 'w', 'g', 'g', 'z', 'z', 'z', 'g', 'g', 'g', 'g', 'g', 'w', 'w' },
                                        { 'w', 'w', 'w', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'z', 'z', 'g', 'g', 'w' },
                                        { 'w', 'w', 'w', 'w', 'w', 'g', 'g', 'g', 'w', 'g', 'z', 'z', 'z', 'g', 'w' },
                                        { 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'g', 'z', 'z', 'z', 'g', 'w' },
                                        { 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'g', 'g', 'z', 'z', 'g', 'w' },
                                        { 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'g', 'z', 'z', 'g', 'w' }};

        char[,] array = new char[,] { { 'g', 'w', 'w', 'w', 'w', 'w', 'w', 'g', 'w', 'w', 'w', 'w', 'w', 'w', 'w' },
                                        { 'g', 'g', 'w', 'w', 'w', 'g', 'g', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w' },
                                        { 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'w', 'g', 'w', 'w', 'w', 'w', 'w', 'w' },
                                        { 'z', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'z', 'z', 'g', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'z', 'g', 'g', 'w', 'w', 'w', 'w' },
                                        { 'z', 'z', 'z', 'g', 'g', 'z', 'z', 'z', 'g', 'g', 'g', 'g', 'g', 'w', 'w' },
                                        { 'z', 'z', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'g', 'z', 'z', 'g', 'g', 'w' },
                                        { 'z', 'g', 'g', 'g', 'w', 'g', 'g', 'g', 'w', 'g', 'z', 'z', 'z', 'g', 'w' },
                                        { 'g', 'g', 'g', 'w', 'w', 'w', 'w', 'w', 'w', 'g', 'z', 'z', 'z', 'g', 'w' },
                                        { 'g', 'g', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'g', 'g', 'z', 'z', 'g', 'w' },
                                        { 'g', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'g', 'z', 'z', 'g', 'w' }};
    }
}
