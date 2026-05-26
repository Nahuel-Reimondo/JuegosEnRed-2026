using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XML_Serializer : MonoBehaviour
{
   public PlayerDataDisplay playerDataDisplay;
   public string path;

   [ContextMenu("Serializar")]
   public void LoadXML()
   {
      var player = XmlDataHandler.CargarXML(path);
      playerDataDisplay.SetPlayerData(player);
   }
}
