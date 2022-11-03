

namespace BattleshipsCoreClient.Prototype
{
    public class RepeatShoot : ShallowPrototype
    {
        public override ShallowPrototype Clone()
        {
            return this.MemberwiseClone() as ShallowPrototype;
        }
    }
}