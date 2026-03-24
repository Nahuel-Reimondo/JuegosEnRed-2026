using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager_Rooms : MonoBehaviourPunCallbacks
{ 
    public static PhotonManager_Rooms Instance;

    public Action OnMasterServer;
    public Action OnLobby;
    public Action OnRoom;
    public Action<List<RoomInfo>> OnRoomList;
    
    void Awake()
    {
       ResolveSingleton();
       
       PhotonNetwork.ConnectUsingSettings();
    }

    private void ResolveSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server");
        PhotonNetwork.JoinLobby();

        OnMasterServer?.Invoke();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        OnLobby?.Invoke();
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        OnRoom?.Invoke();
        
        string roomName = PhotonNetwork.CurrentRoom.Name;
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        bool isMaster = PhotonNetwork.IsMasterClient;
       
        Debug.Log("Joined Room: " + roomName + ", PlayerCount:" + playerCount + ", IsMasterClient: " + isMaster);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        OnRoomList?.Invoke(roomList);
    }

}
