using BattleshipsCore.Game.ShootingStrategy;

namespace BattleshipsCoreClient.Prototype
{
    public class RepeatShoot : ShallowPrototype
    {
        public ShootingStrategy ShootType;
        public RepeatShoot(ShootingStrategy shootType)
        {
            ShootType = shootType;
        }
        public override ShootingStrategy Clone()
        {
            return ShootType;
        }
    }
}