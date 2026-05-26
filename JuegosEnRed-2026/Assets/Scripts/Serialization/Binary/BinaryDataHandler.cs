using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class BinaryDataHandler
{
    // 1. MÉTODO DE SERIALIZACIÓN (Guardar objeto en formato Binario)
    public static void GuardarBinario(string rutaArchivo, PlayerData_Binary datos)
    {
        // File.Open crea el archivo binario en el disco
        using (Stream stream = File.Open(rutaArchivo, FileMode.Create, FileAccess.ReadWrite))
        {
            // BinaryWriter traduce los tipos nativos de C# a bytes puros
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(datos.Nombre);      // Escribe el string (antepone la longitud automáticamente)
                writer.Write(datos.Nivel);       // Escribe 4 bytes (Int32)
                writer.Write(datos.VidaActual);  // Escribe 4 bytes (Float/Single)
                
                // Para las listas, primero debemos guardar cuántos elementos hay
                writer.Write(datos.Inventario.Count); 
                foreach (string item in datos.Inventario)
                {
                    writer.Write(item);
                }
            }
        }
        Debug.Log($"Datos serializados correctamente en Binario: {rutaArchivo}");
    }

    // 2. MÉTODO DE DESERIALIZACIÓN (Leer el flujo de bytes en el ORDEN CORRECTO)
    public static PlayerData_Binary CargarBinario(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo))
        {
            Debug.LogError($"El archivo binario no existe: {rutaArchivo}");
            return null;
        }

        using (Stream stream = File.Open(rutaArchivo, FileMode.Open))
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                // CRÍTICO: Leer exactamente en el mismo orden en que se escribió
                string nombre = reader.ReadString();
                int nivel = reader.ReadInt32();
                float vidaActual = reader.ReadSingle(); // ReadSingle lee un float de 4 bytes

                // Reconstruimos la lista leyendo la cantidad de elementos guardada
                int cantidadItems = reader.ReadInt32();
                List<string> inventario = new List<string>();
                for (int i = 0; i < cantidadItems; i++)
                {
                    inventario.Add(reader.ReadString());
                }

                // Retornamos el objeto completamente reconstruido en memoria
                return new PlayerData_Binary(nombre, nivel, vidaActual, inventario);
            }
        }
    }
}