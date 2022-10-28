using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCoreClient.Extensions
{
    public static class ColorPicker
    {
        public static Color ToColor(this TileType type)
        {
            return type switch
            {
                TileType.Ground => Color.SandyBrown,
                TileType.Water => Color.LightBlue,
                TileType.Grass => Color.LightGreen,
                TileType.Ship => Color.Blue,
                TileType.Hit => Color.Red,
                TileType.Miss => Color.DarkGray,
                TileType.Unavailable => Color.LightGray,
                TileType.Destroyed => Color.Black,

                _ => Color.LightBlue,
            };
        }
    }
}
