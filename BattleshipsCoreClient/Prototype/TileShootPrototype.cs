
using BattleshipsCore.Game.ShootingStrategy;

namespace BattleshipsCoreClient.Prototype
{
    public class TileShootPrototype
    {
        SingleTileShooting SingleTileShoot;
        AreaShooting AreaShoot;
        HorizontalLineShooting horizontalLineShoot;
        VerticalLineShooting verticalLineShoot;

        public TileShootPrototype(SingleTileShooting singleTileShoot, AreaShooting areaShoot, HorizontalLineShooting horizontalLineShoot, VerticalLineShooting verticalLineShoot)
        {
            SingleTileShoot = singleTileShoot;
            AreaShoot = areaShoot;
            this.horizontalLineShoot = horizontalLineShoot;
            this.verticalLineShoot = verticalLineShoot;
        }

        public SingleTileShooting CloneSingle()
        {             
            return SingleTileShoot;
        }
        public AreaShooting CloneArena()
        {
            return AreaShoot;
        }
        public VerticalLineShooting CloneVertical()
        {
            return verticalLineShoot;
        }
        public HorizontalLineShooting CloneHorizontal()
        {
            return horizontalLineShoot;
        }
    }
}