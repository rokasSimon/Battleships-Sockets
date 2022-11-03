namespace BattleshipsCore.Interfaces
{
    public interface IMessageParser
    {
        TRequest ParseRequest<TRequest>(string message) where TRequest : Request;
        TMessage ParseResponse<TMessage>(string message) where TMessage : Message;
        string SerializeMessage(Message message);
    }
}
