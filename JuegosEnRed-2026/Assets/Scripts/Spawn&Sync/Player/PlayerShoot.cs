using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerShoot : MonoBehaviourPun
{
   [SerializeField] private Transform shootPoint;
   [SerializeField] private GameObject bulletPrefab;
    
    
    void Update()
    {
        if (!photonView.IsMine)
            return;

        if (Input.GetButtonDown("Fire1"))
        {
            PhotonNetwork.Instantiate(bulletPrefab.name, shootPoint.position, shootPoint.rotation);
        }
    }
}
