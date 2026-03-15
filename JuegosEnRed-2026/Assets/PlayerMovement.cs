using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    void Update()
    {
        if (!photonView.IsMine)
            return;
    }
}
