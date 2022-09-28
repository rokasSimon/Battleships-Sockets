using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Server
{
    internal class UnknownMessageException : Exception
    {
        public UnknownMessageException()
        {

        }

        public UnknownMessageException(string message) : base(message)
        {

        }
    }
}
