using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Binary_Serialization : MonoBehaviour
{
    [SerializeField] PlayerDataDisplay playerDataDisplay;
    [SerializeField] string fileName;
    
    [SerializeField] PlayerData_Binary playerData_Binary;

    private string completePath;
    
    void Awake()
    {
        // Generamos la ruta usando persistentDataPath.
        // Path.Combine se encarga de poner las barras diagonales correctas según el SO (Windows / Android / Mac)
        //completePath = Path.Combine(Application.persistentDataPath, fileName);
        completePath = Path.Combine(Application.dataPath, fileName);
    }


    [ContextMenu("Load from File")]
    public void LoadFromFile()
    {
        playerDataDisplay.SetPlayerData( BinaryDataHandler.CargarBinario(completePath) );
    }
    
    [ContextMenu("Serialize Inspector")]
    public void SaveFile()
    {
        BinaryDataHandler.GuardarBinario(completePath, playerData_Binary);
    }
}
