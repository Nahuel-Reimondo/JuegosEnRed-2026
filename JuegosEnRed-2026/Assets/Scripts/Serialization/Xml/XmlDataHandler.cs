using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public static class XmlDataHandler
{
    // 1. MÉTODO DE SERIALIZACIÓN (Guardar objeto en archivo XML)
    public static void GuardarXML(string rutaArchivo, PlayerData_Example_XML datos)
    {
        // Creamos el serializador indicando el tipo de objeto que va a manejar
        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData_Example_XML));

        // StreamWriter se encarga de la escritura física del archivo de texto
        using (StreamWriter writer = new StreamWriter(rutaArchivo))
        {
            serializer.Serialize(writer, datos);
        }
        
        Debug.Log($"Datos serializados correctamente en XML: {rutaArchivo}");
    }

    // 2. MÉTODO DE DESERIALIZACIÓN (Convertir archivo XML de vuelta a Objeto)
    public static PlayerData_Example_XML CargarXML(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo))
        {
            Debug.LogError($"El archivo no existe en la ruta: {rutaArchivo}");
            return null;
        }

        XmlSerializer serializer = new XmlSerializer(typeof(PlayerData_Example_XML));

        // StreamReader se encarga de leer el archivo de texto
        using (StreamReader reader = new StreamReader(rutaArchivo))
        {
            // Deserializamos y casteamos explícitamente al tipo original
            PlayerData_Example_XML datosCargados = (PlayerData_Example_XML)serializer.Deserialize(reader);
            return datosCargados;
        }
    }
}