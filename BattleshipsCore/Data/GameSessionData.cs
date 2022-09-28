#nullable disable

namespace BattleshipsCore.Data
{
    public class GameSessionData
    {
        public Guid SessionKey { get; set; }
        public string SessionName { get; set; }
        public bool Active { get; set; }
        public List<string> PlayerNames { get; set; }
    }
}
