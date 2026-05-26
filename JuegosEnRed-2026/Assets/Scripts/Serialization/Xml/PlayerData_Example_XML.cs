using System.Collections.Generic;
using System.Xml.Serialization;

// Podemos usar atributos para renombrar el nodo raíz si quisiéramos
[XmlRoot("Jugador")]
public class PlayerData_Example_XML
{
    public string Nombre { get; set; }
    public int Nivel { get; set; }
    public float VidaActual { get; set; }
    
    // Las listas se serializan de forma jerárquica automáticamente
    [XmlArray("Inventario")]
    [XmlArrayItem("Item")]
    public List<string> Inventario { get; set; }

    // Constructor vacío obligatorio para XML
    public PlayerData_Example_XML() { }

    // Constructor auxiliar para crear instancias fácilmente
    public PlayerData_Example_XML(string nombre, int nivel, float vida, List<string> items)
    {
        Nombre = nombre;
        Nivel = nivel;
        VidaActual = vida;
        Inventario = items;
    }
}