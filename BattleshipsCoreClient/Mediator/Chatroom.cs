using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCoreClient.Mediator
{
    public class ChatRoom : AbstractChatroom
    {
        private Dictionary<string, AsyncSocketClient> _participants =
            new Dictionary<string, AsyncSocketClient>();

        public override void Register(AsyncSocketClient participant)
        {
            if (!_participants.ContainsValue(participant))
            {
                _participants[participant.PlayerName] = participant;
            }

            participant.Chatroom = this;
        }

        public override void Send(string from, string to, string message)
        {
            AsyncSocketClient participant = _participants[to];
            participant?.Receive(from, message);
        }
    }
}
