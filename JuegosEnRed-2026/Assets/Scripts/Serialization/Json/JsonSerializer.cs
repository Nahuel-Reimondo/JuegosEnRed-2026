using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class JsonSerializer : MonoBehaviour
{
    public PlayerDataDisplay playerDataDisplay;
    public string path;

    [ContextMenu("Serializar")]
    public void LoadJson()
    {
        var player = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayerData_Json>(File.ReadAllText(path));
        playerDataDisplay.SetPlayerData(player);
    }
}
