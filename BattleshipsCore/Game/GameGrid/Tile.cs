using System.Text.Json.Serialization;

namespace BattleshipsCore.Game.GameGrid
{
    public struct Tile
    {
        [JsonPropertyName("t")]
        public TileType Type { get; init; }

        public Tile(TileType type)
        {
            Type = type;
        }
    }
}
