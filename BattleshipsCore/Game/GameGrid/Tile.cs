using Newtonsoft.Json;

namespace BattleshipsCore.Game.GameGrid
{
    public  class Tile
    {
        [JsonProperty("t")]
        public TileType Type { get; set; }

        public Tile(TileType type)
        {
            Type = type;
        }
    }
}
