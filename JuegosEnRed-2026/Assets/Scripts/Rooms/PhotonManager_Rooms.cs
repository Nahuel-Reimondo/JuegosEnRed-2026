using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager_Rooms : MonoBehaviourPunCallbacks
{ 
    public static PhotonManager_Rooms Instance;

    [SerializeField] private string gameSceneName;
    
    public Action OnMasterServer;
    public Action OnLobby;
    public Action OnRoom;
    public Action<List<RoomInfo>> OnRoomList;
    
    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();
    
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
        
        PhotonNetwork.AutomaticallySyncScene = true;
        if (isMaster)
        {
            PhotonNetwork.LoadLevel(gameSceneName);
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        //Go back to main menu!
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print( "Room List: " + roomList.Count);
        
        UpdateCachedRoomList(roomList);

        if (roomList != null && roomList.Count > 0)
        {
            OnRoomList?.Invoke(cachedRoomList.Values.ToList());
        }
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }
    
    public List<RoomInfo> GetRoomList() => cachedRoomList.Values.ToList();

}
