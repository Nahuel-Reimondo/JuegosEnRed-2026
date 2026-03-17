using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private float movSpeed;
    [SerializeField] private float rotSpeed;
    
    void Update()
    {
        if (!photonView.IsMine)
             return;
        
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        Vector3 movement = new Vector3(0, 0, Input.GetAxis("Vertical"));
        this.transform.Translate(movement * Time.deltaTime * movSpeed, Space.Self);
        
        Vector3 rotation = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        this.transform.Rotate(rotation * Time.deltaTime * rotSpeed, Space.Self);
        
    }
}
