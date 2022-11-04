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
                TileType.Grass => Color.Green,
                TileType.Ship => Color.Blue,
                TileType.Hit => Color.Red,
                TileType.Tank=> Color.Brown,
                TileType.Miss => Color.DarkGray,
                TileType.Unavailable => Color.LightGray,
                TileType.Destroyed => Color.Black,
                TileType.NarrowBoat => Color.Purple,
                TileType.Cruise => Color.DarkRed,
                TileType.Tanker => Color.DarkOrange,

                _ => Color.LightBlue,
            };
        }
    }
}
