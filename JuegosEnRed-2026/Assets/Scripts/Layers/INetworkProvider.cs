using System;

public interface INetworkProvider
{
    event Action OnConnectedToMasterEvent;
    event Action OnJoinedRoomEvent;
    event Action<string> OnJoinRoomFailedEvent;

    void ConnectUsingSettings();
    void JoinRandomOrCreateRoom();
    void LeaveRoom();
}