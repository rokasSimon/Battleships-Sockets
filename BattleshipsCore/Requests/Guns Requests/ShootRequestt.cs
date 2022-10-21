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
    public class ShootRequestt : GunRequest
    {
        public override MessageType Type => MessageType.Shoot;

        public string Initiator { get; set; }
        public Vec2 Pos { get; set; }
        public ShootRequestt(string initiator, Vec2 pos) : base(initiator, pos)
        {
            Initiator = initiator;
            Pos = pos;
        }

        public override Message Execute()
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(Initiator);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new FailResponse();


            
            var (newGameState1, tileUpdatee) = thisPlayer.JoinedSession.Shoot(Initiator, Pos);
            if (newGameState1 == GameState.Unknown) return new FailResponse();


            return new SendTileUpdateResponse(newGameState1, tileUpdatee);
        }
    }
}
