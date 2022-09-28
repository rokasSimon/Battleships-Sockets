using System.Net.Sockets;

namespace BattleshipsCore.Server
{
    public class SocketStateData
    {
        public const int ClientBufferSize = 4096;

        public Guid Id { get; init; }
        public Socket Socket { get; init; }
        public byte[] Buffer { get; init; } = new byte[ClientBufferSize];

        public SocketStateData(Socket socket)
        {
            Socket = socket;
            Id = Guid.NewGuid();
        }
    }
}
