using BattleshipsCore.Data.Surface;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Server;

namespace BattleshipsCore.Game
{
    internal class PlayerMapInstance
    {
        public PlayerGameState State { get; set; }
        public Vec2 Size { get; init; }
        public Tile[,] OriginalGrid { get; init; }

        public PlayerMapInstance(Vec2 size, bool startFirst)
        {
            State = new JoinedState(this, startFirst);
            Size = size;

            Tile[,] arr = new Tile[size.X, size.Y];
            arr = new GrassCreator().FactoryMethod(size, arr);
            arr = new GroundCreator().FactoryMethod(size, arr);
            arr = new WaterCreator().FactoryMethod(size, arr);
            OriginalGrid = arr;
        }

        public bool UnsetMap()
        {
            var removedTiles = State.UnsetMap();

            return removedTiles != null;
        }
    }
}
