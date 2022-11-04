using BattleshipsCore.Game.ShootingStrategy;

namespace BattleshipsCoreClient.Prototype
{
    public abstract class DeepPrototype
    {
        public abstract SingleTileShooting CloneSingle();
        public abstract AreaShooting CloneArena();
        public abstract VerticalLineShooting CloneVertical();
        public abstract HorizontalLineShooting CloneHorizontal();
       
    }
}