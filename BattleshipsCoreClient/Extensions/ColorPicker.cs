using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCoreClient.Extensions
{
    public static class ColorPicker
    {
        public static Color ToColor(this TileType type)
        {
            return type switch
            {
                TileType.Ground => Color.LightGreen,
                TileType.Ship => Color.Blue,
                TileType.Tank => Color.DarkGreen,
                TileType.Hit => Color.Red,
                TileType.Miss => Color.DarkGray,
                TileType.Unavailable => Color.LightGray,
                TileType.Destroyed => Color.Black,
             

                _ => Color.LightBlue,
            };
        }
    }
}
