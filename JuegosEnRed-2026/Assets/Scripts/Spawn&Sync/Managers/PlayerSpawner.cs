using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    
    void Start()
    {
        PhotonManager.Instance.OnRoom += SpawnPlayer;
    }

    // private void SpawnPlayer()
    // {
    //     Instantiate(playerPrefab, transform.position, Quaternion.identity);
    // }

    private void SpawnPlayer()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity, 0);
    }

}
