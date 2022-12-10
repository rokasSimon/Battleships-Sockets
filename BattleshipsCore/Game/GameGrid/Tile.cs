using Newtonsoft.Json;

namespace BattleshipsCore.Game.GameGrid
{
    public class Tile
    {
        [JsonProperty("t")]
        public TileType Type { get; set; }
        [JsonProperty("is_disabled")]
        public bool IsDisabled { get; set; }

        public Tile(TileType type)
        {
            Type = type;
            IsDisabled = false;
        }
    }
}
