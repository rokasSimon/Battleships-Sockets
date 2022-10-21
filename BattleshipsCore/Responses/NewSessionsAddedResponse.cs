using BattleshipsCore.Data;
using BattleshipsCore.Game;
using BattleshipsCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Responses
{
    public class NewSessionsAddedResponse : Message
    {

        public override MessageType Type => MessageType.NewSessionsAdded;

        public int SessionCount { get; set; }

        public NewSessionsAddedResponse(int sessionCount)
        {
            SessionCount = sessionCount;
        }
    }
}