using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipsCore.Game;

namespace BattleshipsCore.Requests.Guns_Requests
{
    public class BombRequest : GunRequest
    {
        public override MessageType Type => MessageType.Bomb;

        public string Initiator { get; set; }
        public Vec2 Pos { get; set; }
        public Vec2 Pos2 { get; set; }
        public Vec2 Pos3 { get; set; }
        public Vec2 Pos4 { get; set; }
        public Vec2 Pos5 { get; set; }
        public BombRequest(string initiator, Vec2 pos) : base(initiator, pos)
        {
            Initiator = initiator;
            Pos = pos;
            Pos2 = new Vec2(pos.X, pos.Y - 1);
            Pos3 = new Vec2(pos.X, pos.Y + 1);
            Pos4 = new Vec2(pos.X - 1, pos.Y);
            Pos5 = new Vec2(pos.X + 1, pos.Y);
        }
        public override Message Execute()
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(Initiator);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new FailResponse();



            TileUpdate[] tileUpdate = new TileUpdate[5];
            Vec2[] pos = { Pos, Pos2, Pos3, Pos4, Pos5 };
            var (newGameState1, tileUpdatee) = thisPlayer.JoinedSession.Bomb(Initiator, pos);
            if (newGameState1 == GameState.Unknown) return new FailResponse();


            return new SendTilesUpdateResponse(newGameState1, tileUpdatee);
        }

        
        

    }
}
