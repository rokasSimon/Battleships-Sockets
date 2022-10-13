using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game.PlaceableObjects.Guns
{
    public class BombRequest : GunRequest
    {
        public override MessageType Type => MessageType.SetBomb;

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

            var (newGameState, tileUpdate) = thisPlayer.JoinedSession.Shoot(Initiator, Pos);
            //var (newGameState, tileUpdate) = thisPlayer.JoinedSession.Shoot(Initiator, Pos2);
            //reikia iskviesti saudyma ir tileatnaujinima 5 langeliams


            if (newGameState == GameState.Unknown) return new FailResponse();

            return new SendTileUpdateResponse(newGameState, tileUpdate);
        }
    }
}
