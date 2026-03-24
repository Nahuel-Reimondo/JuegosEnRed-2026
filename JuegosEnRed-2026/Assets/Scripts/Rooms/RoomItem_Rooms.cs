using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem_Rooms : MonoBehaviour
{
    [SerializeField] private Button joinButton;
    [SerializeField] private TextMeshProUGUI roomNameText;
    [SerializeField] private TextMeshProUGUI playersText;
    
    public string RoomName => myRoomData.roomName;
    
    private RoomData myRoomData;
    
    public void SetUp(RoomData roomData, Action<RoomItem_Rooms> connectionCallback)
    {
        myRoomData = roomData;

        roomNameText.text = myRoomData.roomName;
        playersText.text = myRoomData.players + "/" + myRoomData.maxPlayers;
        
        
        joinButton.interactable = true;
        joinButton.onClick.AddListener(()=> connectionCallback?.Invoke(this));
    }
}
