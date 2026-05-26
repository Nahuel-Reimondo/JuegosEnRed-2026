using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData_Json : MonoBehaviour
{
    public string Nombre { get; set; }
    public int Nivel { get; set; }
    public float VidaActual { get; set; }
    public List<string> Inventario { get; set; }

    public PlayerData_Json(string nombre, int nivel, float vida, List<string> items)
    {
        Nombre = nombre;
        Nivel = nivel;
        VidaActual = vida;
        Inventario = items;
    }
}
