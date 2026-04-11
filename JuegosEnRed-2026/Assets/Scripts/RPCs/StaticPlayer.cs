using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class StaticPlayer : MonoBehaviourPun
{
    [SerializeField] private TextMeshProUGUI IDText;
    [SerializeField] private Renderer renderer;
    
    [PunRPC]
    public void ChangeColor(string color)
    {
        renderer.material.color = GetColor(color);
    }

    private Color GetColor(string color)
    {
        switch (color)
        {
            case "Blue":
                return Color.blue;
            case "Green":
                return Color.green;
            case "Red":
                return Color.red;
            case "White":
                return Color.white;
            default:
                return Color.black;
            
        }
    }
}
