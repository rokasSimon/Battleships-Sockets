using BattleshipsCore.Game.GameGrid;

namespace BattleshipsCore.Data
{
    public class AllowedUnitData
    {
        public TileType Type { get; set; }
        public string Name { get; set; }
        public int Max { get; set; }
        public int Length { get; set; }
        public int SideBlocks { get; set; }
    }
}
