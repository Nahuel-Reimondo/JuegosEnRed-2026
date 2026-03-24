using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CreateRoomUI_Rooms : MonoBehaviour
{
    [SerializeField] TMP_InputField roomNameInput;
    [SerializeField] TMP_InputField maxPlayersInput;
    [SerializeField] Button createRoomButton;

    public Action OnFinish;
    private string MAP_KEY = "Map";
    private string GAME_MODE_KEY ="GameMode";

    void OnEnable()
    {
        createRoomButton.onClick.AddListener(CreateAndJoin);
    }

    void onDisable()
    {
        createRoomButton.onClick.RemoveAllListeners();
    }

    public void CreateAndJoin()
    {
        int maxPlayersAmount = 4;
        if (int.TryParse(maxPlayersInput.text, out int maxLabelInput))
        {
            maxPlayersAmount = maxLabelInput;
        }

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.MaxPlayers = maxPlayersAmount;
        roomOptions.PlayerTtl = 3000;
        roomOptions.EmptyRoomTtl = 60000;
        roomOptions.BroadcastPropsChangeToAll = true;
        roomOptions.CustomRoomProperties = new Hashtable { { MAP_KEY, 1 }, { GAME_MODE_KEY, 0 } };
        
        PhotonNetwork.CreateRoom(roomNameInput.text, roomOptions);
        
        OnFinish?.Invoke();
        
    }
}
