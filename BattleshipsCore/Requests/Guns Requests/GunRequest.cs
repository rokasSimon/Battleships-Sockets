using BattleshipsCore.Data;
using BattleshipsCore.Game.GameGrid;
using BattleshipsCore.Interfaces;
using BattleshipsCore.Responses;
using BattleshipsCore.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Requests.Guns_Requests
{
    public abstract class GunRequest : Request
    {
        protected GunRequest(string initiator, Vec2 pos) 
        {
            Initiator = initiator;
            Pos = pos;
        }

        public override MessageType Type => MessageType.Shoot;

        public string Initiator { get; set; }
        public Vec2 Pos { get; set; }

        
      

        public override Message Execute()
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(Initiator);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new FailResponse();

            var (newGameState, tileUpdate) = thisPlayer.JoinedSession.Shoot(Initiator, Pos);

            if (newGameState == GameState.Unknown) return new FailResponse();

            return new SendTileUpdateResponse(newGameState, tileUpdate);
        }
    }
}
