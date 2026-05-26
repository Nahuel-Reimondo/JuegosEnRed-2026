using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PlayerDataDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI lifeText;
    [SerializeField] private TextMeshProUGUI inventoryText;
    
    private string PrintInventory(List<string> inventory)
    {
        StringBuilder sb  = new StringBuilder();

        foreach (string s in inventory)
        {
            sb.Append(s);
        }
        
        return sb.ToString();
    }
    
    #region XML
    public void SetPlayerData(PlayerData_Example_XML newPlayerData)
    {
        nameText.text = newPlayerData.Nombre;
        levelText.text = newPlayerData.Nivel.ToString();
        lifeText.text = newPlayerData.VidaActual.ToString("00.00");
        inventoryText.text = PrintInventory(newPlayerData.Inventario);
    }

   
    #endregion
    
    #region Binary
    public void SetPlayerData(PlayerData_Binary newPlayerData)
    {
        nameText.text = newPlayerData.Nombre;
        levelText.text = newPlayerData.Nivel.ToString();
        lifeText.text = newPlayerData.VidaActual.ToString("00.00");
        inventoryText.text = PrintInventory(newPlayerData.Inventario);
    }
    #endregion
    
    #region Json
    public void SetPlayerData(PlayerData_Json newPlayerData)
    {
        nameText.text = newPlayerData.Nombre;
        levelText.text = newPlayerData.Nivel.ToString();
        lifeText.text = newPlayerData.VidaActual.ToString("00.00");
        inventoryText.text = PrintInventory(newPlayerData.Inventario);
    }
    #endregion
    
}
