namespace BattleshipsCore.Game
{
    public enum MessageType : int
    {
        Unknown = 0,
        Ok = 1,
        Fail = 2,

        JoinServer = 3,
        Disconnect = 4,

        JoinSession = 5,
        LeaveSession = 6,

        SendPlayerList = 10,
        SendSessionKey = 11,
        SendSessionList = 12,
        SendSessionData = 13,
        SendMapData = 14,

        CreateSession = 20,

        GetSessionList = 30,
        GetSessionData = 31,
        GetPlayerList = 32,
        GetMapData = 33,

        StartGame = 50,
    }
}
