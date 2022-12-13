namespace BattleshipsCore.Interfaces
{
    public abstract class AcceptableResponse : Message
    {
        public abstract Task Accept(IResponseVisitor v);
    }
}
