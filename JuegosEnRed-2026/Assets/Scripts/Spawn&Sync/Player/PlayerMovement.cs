using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    [SerializeField] private bool usePhysics;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float movSpeed;
    [SerializeField] private float rotSpeed;

    private PhotonView myView;
    
    private void Start()
    {
        myView = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if(!usePhysics)return;
        if (!photonView.IsMine)
            return;
        ProcessPhysicsMovement();
    }

    void Update()
    {
        if (usePhysics)
            return;
        
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
    
    private void ProcessPhysicsMovement()
    {
        // 1. Calcular y aplicar el movimiento (relativo a la rotación actual del objeto)
        float moveInput = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * moveInput * movSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    
        // 2. Calcular y aplicar la rotación
        float rotInput = Input.GetAxis("Horizontal");
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0f, rotInput * rotSpeed * Time.fixedDeltaTime, 0f));
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
    
    
   
}
