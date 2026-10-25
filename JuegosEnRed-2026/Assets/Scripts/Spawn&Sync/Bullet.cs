using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Photon.Pun;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    [SerializeField] private float speed;
    
    void OnEnable()
    {
        Destroy(this.gameObject, 2f);
    }

    void Update()
    {
        this.transform.Translate(transform.forward * this.speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;
        
        var otherView = other.GetComponent<PhotonView>();
        if (otherView == null) return;
        if (otherView.IsMine) return;

        var health = other.GetComponent<HealthComponent>();
        if (health == null) return;
        //health.Damage(30);
        health.DealDamage(30);
        
        PhotonNetwork.Destroy(gameObject);

    }
}
