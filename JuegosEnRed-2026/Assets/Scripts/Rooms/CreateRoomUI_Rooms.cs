using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CreateRoomUI_Rooms : MonoBehaviour
{
    private TMP_InputField roomNameInput;

    public Action<bool> OnFinish;
    private string MAP_KEY = "Map";
    private string GAME_MODE_KEY ="GameMode";

    public void CreateAndJoin()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = 4;
        roomOptions.PlayerTtl = 3000;
        roomOptions.EmptyRoomTtl = 60000;
        roomOptions.BroadcastPropsChangeToAll = true;
        roomOptions.CustomRoomProperties = new Hashtable { { MAP_KEY, 1 }, { GAME_MODE_KEY, 0 } };
        
        PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);
    }
}
