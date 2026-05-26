using System.IO;
using UnityEngine;
using Newtonsoft.Json; // Requiere esta librería (disponible en Unity nativamente)

public static class JsonDataHandler
{
    // 1. MÉTODO DE SERIALIZACIÓN (Guardar objeto en archivo JSON)
    public static void GuardarJSON(string rutaArchivo, PlayerData_Json datos)
    {
        // Formatting.Indented hace que el JSON sea estético (con saltos de línea y tabulaciones).
        // Si quisiéramos ahorrar espacio para la red, usamos Formatting.None
        string jsonTexto = JsonConvert.SerializeObject(datos, Formatting.Indented);

        // Escribimos el string directamente en el archivo
        File.WriteAllText(rutaArchivo, jsonTexto);

        Debug.Log($"Datos serializados correctamente en JSON: {rutaArchivo}");
    }

    // 2. MÉTODO DE DESERIALIZACIÓN (Convertir archivo JSON de vuelta a Objeto)
    public static PlayerData_Json CargarJSON(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo))
        {
            Debug.LogError($"El archivo JSON no existe: {rutaArchivo}");
            return null;
        }

        // Leemos todo el contenido de texto del archivo
        string jsonTexto = File.ReadAllText(rutaArchivo);

        // Deserializamos directamente indicando el tipo genérico <PlayerData>
        PlayerData_Json datosCargados = JsonConvert.DeserializeObject<PlayerData_Json>(jsonTexto);
        
        return datosCargados;
    }
}