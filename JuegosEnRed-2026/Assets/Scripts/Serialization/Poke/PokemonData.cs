using System.Collections.Generic;
using Newtonsoft.Json;

// Esta clase representa la raíz del JSON que devuelve la API
public class PokemonData
{
    // Usamos [JsonProperty] si el nombre en el JSON es diferente al que queremos en C#
    [JsonProperty("name")]
    public string Nombre { get; set; }

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("weight")]
    public int Peso { get; set; }

    [JsonProperty("sprites")]
    public SpriteLinks Imagenes { get; set; }

    [JsonProperty("abilities")]
    public List<AbilitySlot> Habilidades { get; set; }
}

// Clases de soporte para las propiedades anidadas del JSON
public class SpriteLinks
{
    [JsonProperty("front_default")]
    public string SpriteFrontal { get; set; } // URL de la imagen del Pokémon
}

public class AbilitySlot
{
    [JsonProperty("ability")]
    public AbilityInfo InformacionHabilidad { get; set; }
}

public class AbilityInfo
{
    [JsonProperty("name")]
    public string NombreHabilidad { get; set; }
}