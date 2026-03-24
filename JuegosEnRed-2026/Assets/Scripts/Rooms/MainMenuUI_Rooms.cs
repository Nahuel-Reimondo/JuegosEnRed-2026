using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI_Rooms : MonoBehaviour
{
    [SerializeField] private Button connectButton;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject roomsPanel;
    [SerializeField] private CreateRoomUI_Rooms createRoomUI_Rooms;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private SelectRoom_Rooms selectRoom_Rooms;
    
    
    void Start()
    {
        connectButton.onClick.AddListener(()=> ShowRooms());
        PhotonManager_Rooms.Instance.OnMasterServer += ()=> connectButton.interactable = true;
    }

    private void ShowRooms()
    {
        roomsPanel.SetActive(true);
        loadingPanel.SetActive(false);
        mainPanel.gameObject.SetActive(false);

        selectRoom_Rooms.OnRoomSelected += () => ShowLoadingPanel();
        createRoomUI_Rooms.OnFinish += () => ShowLoadingPanel();

    }

    public void ShowLoadingPanel()
    {
        roomsPanel.SetActive(false);
        loadingPanel.SetActive(true);
        mainPanel.gameObject.SetActive(false);
    }

}
