

namespace BattleshipsCoreClient.Prototype
{
    public class RepeatShoot : ShootPrototype
    {
        public override ShootPrototype Clone()
        {
            return this.MemberwiseClone() as ShootPrototype;
        }
    }
}