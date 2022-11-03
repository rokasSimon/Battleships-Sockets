namespace BattleshipsCore.Game.PlaceableObjects
{
    public abstract class AbstractLevelUnitFactory
    {
        public abstract Ship CreateShipUnit(string name, int length, int sideBlocks, int max);
        public abstract Tank CreateTankUnit(int type, string name, int max);
    }
}
