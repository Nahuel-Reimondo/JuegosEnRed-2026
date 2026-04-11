using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ColorsRPC : MonoBehaviourPunCallbacks
{

    private List<PhotonView> allPhotonViews = new List<PhotonView>();
    
    private PhotonView selectedPhotonView;
    
    
    public override void OnJoinedRoom()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void AddPlayer(PhotonView pv)
    {
        allPhotonViews.Add(pv);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedPhotonView  = allPhotonViews[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedPhotonView  = allPhotonViews[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedPhotonView  = allPhotonViews[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedPhotonView  = allPhotonViews[3];
            
        }
    }

    [ContextMenu("All Red")]
    public void PaintAllRed()
    {
        foreach (PhotonView photonView in allPhotonViews)
        {
            photonView.RPC("ChangeColor", RpcTarget.All, "Red");
        }
    }

    [ContextMenu("Selected Green")]
    public void SelectedGreen()
    {
        //selectedPhotonView.RPC("ChangeColor", selectedPhotonView.Controller, "Green");
        selectedPhotonView.RPC("ChangeColor", RpcTarget.All, "Green");
    }

}
