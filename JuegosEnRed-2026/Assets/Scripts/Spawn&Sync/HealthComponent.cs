using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class HealthComponent : MonoBehaviourPun
{
    [SerializeField]private int maxHealth;
    [Space]
    [SerializeField] private int currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth;
    }
    
    // 1. El Jugador A llama a esta función cuando golpea al Jugador B
    public void DealDamage(int damageAmount)
    {
        // En lugar de restar la vida localmente, enviamos un RPC al dueño de este PhotonView
        photonView.RPC(nameof(TakeDamageRPC), RpcTarget.All, damageAmount);
    }

    // 2. Este RPC se ejecuta a nivel local de cada instancia de juego
    [PunRPC]
    private void TakeDamageRPC(int damageAmount)
    {
        // El dueño actualiza su propia vida
        currentHealth -= damageAmount;
        
        if (currentHealth <= 0) 
        {
            Destroy(gameObject);   
        }
    }
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(currentHealth);
    //     }
    //     else
    //     {
    //         currentHealth = (int)stream.ReceiveNext();
    //     }
    // }
}
