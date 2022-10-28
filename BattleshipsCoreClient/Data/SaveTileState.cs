using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Data
{
    public class SaveTileState : IEquatable<SaveTileState?>
    {
        public int x { get; set; }
        public int y { get; set; }
        public string state { get; set; }

        public SaveTileState(int x, int y, string state)
        {
            this.x = x;
            this.y = y;
            this.state = state;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as SaveTileState);
        }

        public bool Equals(SaveTileState? other)
        {
            return other is not null &&
                   x == other.x &&
                   y == other.y &&
                   state == other.state;
        }
    }
}
