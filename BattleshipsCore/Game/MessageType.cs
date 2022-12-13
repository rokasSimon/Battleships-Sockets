namespace BattleshipsCore.Game
{
    public enum MessageType : int
    {
        Unknown,
        Ok,
        Fail,

        JoinServer,
        JoinedServer,
        Disconnect,
        Disconnected,

        JoinSession,
        JoinedSession,
        LeaveSession,
        LeftSession,

        SendPlayerList,
        SendSessionKey,
        SendSessionList,
        SendSessionData,
        SendMapData,
        SendTileUpdate,
        SendText,

        CreateSession,

        GetSessionList,
        GetSessionData,
        GetPlayerList,
        GetMapData,
        GetOpponentMap,
        GetMyTurn,

        StartGame,
        StartedGame,
        StartBattle,
        StartedBattle,

        SetTiles,
        UnsetTiles,
        Shoot,

        ActiveTurn,
        InactiveTurn,
        LostGame,
        WonGame,

        NewSessionsAdded,
    }
}
