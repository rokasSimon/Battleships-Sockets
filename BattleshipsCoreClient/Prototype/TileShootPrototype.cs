
using BattleshipsCore.Game.ShootingStrategy;

namespace BattleshipsCoreClient.Prototype
{
    public class TileShootPrototype : DeepPrototype
    {
        SingleTileShooting SingleTileShoot;
        AreaShooting AreaShoot;
        HorizontalLineShooting HorizontalLineShoot;
        VerticalLineShooting VerticalLineShoot;

        public TileShootPrototype(SingleTileShooting singleTileShoot, AreaShooting areaShoot, HorizontalLineShooting horizontalLineShoot, VerticalLineShooting verticalLineShoot)
        {
            SingleTileShoot = singleTileShoot;
            AreaShoot = areaShoot;
            this.HorizontalLineShoot = horizontalLineShoot;
            this.VerticalLineShoot = verticalLineShoot;
        }

        public override SingleTileShooting CloneSingle()
        {             
            return SingleTileShoot;
        }
        public override AreaShooting CloneArena()
        {
            return AreaShoot;
        }
        public override VerticalLineShooting CloneVertical()
        {
            return VerticalLineShoot;
        }
        public override HorizontalLineShooting CloneHorizontal()
        {
            return HorizontalLineShoot;
        }
    }
}