using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class PropsSpawner : MonoBehaviourPun
{
    [SerializeField] private Vector2 minPos;
    [SerializeField] private Vector2 maxPos;
    [SerializeField] private float height = 1f;
    [SerializeField] private GameObject propPrefab;
    [SerializeField] private int amount;

    void Start()
    {
        PhotonManager.Instance.OnRoom += CreateObjects;
    }

    private void CreateObjects()
    {
        if(!PhotonNetwork.IsMasterClient)
            return;
        
        for (int i = 0; i < amount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(minPos.x, maxPos.x),height, Random.Range(minPos.y, maxPos.y));
        
            var go = PhotonNetwork.InstantiateRoomObject(propPrefab.name, pos, Quaternion.identity, 0);
            go.SetActive(true);
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.Draw(transform.position, new Vector3(1,1,1));
    // }
}
