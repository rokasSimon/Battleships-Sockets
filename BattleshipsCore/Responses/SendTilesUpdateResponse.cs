using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;

namespace BattleshipsCore.Responses
{
    public class SendTilesUpdateResponse : Message
    {
        public override MessageType Type => MessageType.SendTilesUpdate;

        public GameState GameState { get; set; }
        public TileUpdate[]? TileUpdate { get; set; }

        public SendTilesUpdateResponse(GameState state, TileUpdate[]? tileUpdate)
        {
            GameState = state;
            TileUpdate = tileUpdate;
        }
    }
}
