using System;

public interface INetworkMediator
{
    event Action OnConnected;
    event Action OnJoinedRoom;
    event Action<string> OnJoinFailed;

    void Connect();
    void FindOrCreateMatch();
    void DisconnectAndLeave();
}