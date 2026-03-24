using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class SelectRoom_Rooms : MonoBehaviour
{
    [SerializeField] private GameObject selectRoomPanel;
    [SerializeField] private RectTransform roomsListRect;
    [SerializeField] private RoomItem_Rooms roomItem;
    [SerializeField] private Button joinRandomRoomButton;
    [SerializeField] private Button createRoomButton;
    [SerializeField] private GameObject createRoomPanel;
    
    private List<RoomItem_Rooms> rooms; 
    
    private void OnEnable()
    {
        PhotonManager_Rooms.Instance.OnRoomList += UpdateRoomList;
        joinRandomRoomButton.onClick.AddListener(
            ()=>PhotonManager_Rooms.Instance.JoinRandomRoom());
        
        createRoomButton.onClick.AddListener(DisplayCreateRoomPanel);
    }

    private void OnDisable()
    {
        PhotonManager_Rooms.Instance.OnRoomList -= UpdateRoomList;
        joinRandomRoomButton.onClick.RemoveAllListeners();
        createRoomButton.onClick.RemoveAllListeners();
        
    }

    private void UpdateRoomList(List<RoomInfo> obj)
    {
        ClearList();
        foreach (RoomInfo roomInfo in obj)
        {
            CreateRoomItem(roomInfo);   
        }
    }

    private void ClearList()
    {
        foreach (var room in rooms)
        {
            Destroy(room.gameObject);
        }
        rooms.Clear();
    }

    private void CreateRoomItem(RoomInfo roomInfo)
    {
        RoomItem_Rooms roomItemInstance = Instantiate(roomItem, roomsListRect.transform);
        roomItemInstance.SetUp(ParseRoomData(roomInfo), JoinSelectedRoom);
        roomItemInstance.gameObject.SetActive(true);
        
        rooms.Add(roomItemInstance);
    }

    private RoomData ParseRoomData(RoomInfo roomInfo)
    {
        RoomData roomData = new RoomData();
        roomData.roomName = roomInfo.Name;
        roomData.players = roomInfo.PlayerCount;
        roomData.maxPlayers = roomInfo.MaxPlayers;
        roomData.isOpen = roomInfo.IsOpen;
        
        return roomData;
    }

    private void JoinSelectedRoom(RoomItem_Rooms roomItem)
    {
        PhotonManager_Rooms.Instance.JoinRoom(roomItem.name);
    }

    
    
    
    private void DisplayCreateRoomPanel()
    {
        selectRoomPanel.SetActive(false);
        createRoomPanel.SetActive(true);
    }
    
}

public struct RoomData
{
    public string roomName;
    public int players;
    public int maxPlayers;
    public bool isOpen;
}
