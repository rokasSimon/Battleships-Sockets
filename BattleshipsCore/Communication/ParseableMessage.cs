using BattleshipsCore.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace BattleshipsCore.Communication
{
    public abstract class ParseableMessage
    {
        public string Data { get; set; }

        public ParseableMessage(string data)
        {
            Data = data;
        }

        public abstract TMessage? Parse<TMessage>() where TMessage : Message;
        public abstract bool IsParseable();
    }
}
