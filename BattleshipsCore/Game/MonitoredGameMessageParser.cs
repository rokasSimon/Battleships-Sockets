using BattleshipsCore.Interfaces;
using BattleshipsCore.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Game
{
    internal class MonitoredGameMessageParser : IMessageParser
    {
        private IMessageParser _parser;

        public MonitoredGameMessageParser() {
            _parser = new GameMessageParser();
        }

        public TRequest ParseRequest<TRequest>(string message) where TRequest : Request
        {
            ServerLogger.Instance.LogInfo("[Proxy logging] ParseRequest: " + message);
            return _parser.ParseRequest<TRequest>(message);
        }

        public TMessage ParseResponse<TMessage>(string message) where TMessage : Message
        {
            ServerLogger.Instance.LogInfo("[Proxy logging] ParseResponse: " + message);
            return _parser.ParseResponse<TMessage>(message);
        }

        public string SerializeMessage(Message message, bool useXml = false)
        {
            ServerLogger.Instance.LogInfo("[Proxy logging] SerializeMessage: " + message);
            return _parser.SerializeMessage(message, useXml: useXml);
        }
    }
}
