using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Requests.Guns_Requests
{
    public class DoubleShotRequest : GunRequest
    {

        public override MessageType Type => MessageType.DoubleShoot;

        public string Initiator { get; set; }
        public Vec2 Pos { get; set; }
        public Vec2 Pos2 { get; set; }
        public DoubleShotRequest(string initiator, Vec2 pos) : base(initiator, pos)
        {
            Initiator = initiator;
            Pos = pos;
            Pos2 = new Vec2(pos.X, pos.Y - 1);
        }

        public override Message Execute()
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(Initiator);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new FailResponse();


            TileUpdate[] tileUpdate = new TileUpdate[2];
            Vec2[] pos = { Pos, Pos2 };
            var (newGameState1, tileUpdatee) = thisPlayer.JoinedSession.Bomb(Initiator, pos);
            if (newGameState1 == GameState.Unknown) return new FailResponse();


            return new SendTilesUpdateResponse(newGameState1, tileUpdatee);

        }
    }
}
