using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData_Binary
{
    public string Nombre;
    public int Nivel;
    public float VidaActual;
    public List<string> Inventario;

    public PlayerData_Binary(string nombre, int nivel, float vida, List<string> items)
    {
        Nombre = nombre;
        Nivel = nivel;
        VidaActual = vida;
        Inventario = items;
    }
}