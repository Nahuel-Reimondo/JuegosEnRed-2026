using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI_Rooms : MonoBehaviour
{
    [SerializeField] private Button connectButton;
    [SerializeField] private GameObject roomsPanel;
    [SerializeField] private GameObject loadingPanel;
    
    
    void Start()
    {
        connectButton.onClick.AddListener(()=> ShowRooms());
        PhotonManager_Rooms.Instance.OnMasterServer += ()=> connectButton.interactable = true;
    }

    private void ShowRooms()
    {
        roomsPanel.SetActive(true);
        loadingPanel.SetActive(false);
        connectButton.gameObject.SetActive(false);
    }

    public void ShowLoadingPanel()
    {
        roomsPanel.SetActive(false);
        loadingPanel.SetActive(true);
        connectButton.gameObject.SetActive(false);
    }

}
