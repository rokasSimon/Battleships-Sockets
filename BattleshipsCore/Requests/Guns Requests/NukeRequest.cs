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
    public class NukeRequest : GunRequest
    {
        public NukeRequest(string initiator, Vec2 pos) : base(initiator, pos)
        {
        }

        public string Initiator { get; set; }
        private Vec2[] Pos { get; set; }

        public Vec2[] GetPost()
        {
            return Pos;
        }
        public void SetPos(Vec2[] pos)
        {
            Pos = pos;
        }       
        public  Message Execute()
        {
            var thisPlayer = ServerGameStateManager.Instance.GetPlayer(Initiator);

            if (thisPlayer == null ||
                thisPlayer.JoinedSession == null ||
                !thisPlayer.JoinedSession.BattleActive) return new FailResponse();

            /* var (newGameState, tileUpdate) = thisPlayer.JoinedSession.NukeShoot(Initiator, Pos);

             if (newGameState == GameState.Unknown) return new FailResponse();

             return new SendTilesUpdateResponse(newGameState, tileUpdate);*/
            return null;
        }

       
    }
}
