using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PhotonManager : MonoBehaviourPunCallbacks
{
   void Awake()
   {
        PhotonNetwork.ConnectUsingSettings();   
   }

   public override void OnConnectedToMaster()
   {
       Debug.Log("Connected to Server");
       PhotonNetwork.JoinLobby();
   }

   public override void OnJoinedLobby()
   {
       Debug.Log("Joined Lobby");
       PhotonNetwork.JoinRandomOrCreateRoom(roomName: "My Room");
   }

   public override void OnJoinedRoom()
   {
       string roomName = PhotonNetwork.CurrentRoom.Name;
       int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
       Debug.Log("Joined Room: " + roomName + ", PlayerCount:" + playerCount);
   }
}
