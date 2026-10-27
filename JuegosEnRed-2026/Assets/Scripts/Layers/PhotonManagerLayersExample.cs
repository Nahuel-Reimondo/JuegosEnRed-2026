using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonManagerLayersExample : MonoBehaviourPunCallbacks, INetworkProvider
{
    public event Action OnConnectedToMasterEvent;
    public event Action OnJoinedRoomEvent;
    public event Action<string> OnJoinRoomFailedEvent;

    public void ConnectUsingSettings()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void JoinRandomOrCreateRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        OnConnectedToMasterEvent?.Invoke();
    }

    public override void OnJoinedRoom()
    {
        OnJoinedRoomEvent?.Invoke();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        OnJoinRoomFailedEvent?.Invoke(message);
    }
}