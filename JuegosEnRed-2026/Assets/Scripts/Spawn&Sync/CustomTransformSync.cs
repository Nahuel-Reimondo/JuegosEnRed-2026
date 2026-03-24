using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CustomTransformSync : MonoBehaviourPun, IPunObservable
{
    private Vector3 networkPos;
    private Quaternion networkRot;

    private void Update()
    {
        if (!photonView.IsMine)
        {
            this.transform.position = 
                Vector3.Lerp(this.transform.position, networkPos, Time.deltaTime);
            this.transform.rotation =
                Quaternion.Lerp(this.transform.rotation, networkRot, Time.deltaTime);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //We own this player: send the others our data
            //stream.SendNext((int)controllerScript._characterState);
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            //Network player, receive data
            //controllerScript._characterState = (CharacterState)(int)stream.ReceiveNext();
            networkPos = (Vector3)stream.ReceiveNext();
            networkRot = (Quaternion)stream.ReceiveNext();
        }
    }
    
}
