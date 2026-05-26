using System.Collections;
using UnityEngine;
using UnityEngine.Networking; // Obligatorio para UnityWebRequest
using Newtonsoft.Json;       // Obligatorio para Newtonsoft

public class PokeApiConsumer : MonoBehaviour
{
    [Header("Configuración de Búsqueda")]
    [Tooltip("Escribí el nombre del Pokémon en minúsculas (ej: ditto, pikachu, charizard)")]
    public string nombrePokemon = "pikachu";

    private string urlBase = "https://pokeapi.co/api/v2/pokemon/";

    private void Update()
    {
        // Al presionar la barra espaciadora, ejecutamos la consulta
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string urlFinal = urlBase + nombrePokemon.ToLower().Trim();
            StartCoroutine(ConsumirApiPokemon(urlFinal));
        }
    }

    // Corrutina encargada de la petición asíncrona por red
    private IEnumerator ConsumirApiPokemon(string url)
    {
        Debug.Log($"<color=yellow>Consultando a la API: {url} ...</color>");

        // 1. Preparamos la petición GET usando el protocolo HTTP
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // 2. Enviamos la petición y esperamos (yield) hasta que el servidor responda
            yield return request.SendWebRequest();

            // 3. Validamos posibles errores de red o del servidor (ej: 404 si el Pokémon no existe)
            if (request.result == UnityWebRequest.Result.ConnectionError 
                ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error al consultar la API: {request.error} " +
                               $"(Código HTTP: {request.responseCode})");
            }
            else
            {
                // 4. Si todo salió bien, extraemos el texto plano (JSON bruto)
                string jsonRecibido = request.downloadHandler.text;
                
                // Imprimimos el JSON crudo en la consola para que los alumnos lo vean
                Debug.Log($"<color=cyan>JSON Crudo Recibido:</color>\n{jsonRecibido}");

                // 5. PARSEO / DESERIALIZACIÓN: La magia de Newtonsoft
                // Transformamos el string de texto en nuestro objeto PokemonData de C#
                PokemonData pokemon = JsonConvert.DeserializeObject<PokemonData>(jsonRecibido);

                // 6. HACER ALGO CON LOS DATOS: Mostramos los resultados procesados en la consola
                MostrarDatosEnConsola(pokemon);
            }
        }
    }

    private void MostrarDatosEnConsola(PokemonData pokemon)
    {
        string info = $"<b><color=green>¡POKÉMON ENCONTRADO!</color></b>\n" +
                      $"Nombre: {pokemon.Nombre.ToUpper()} (ID: {pokemon.Id})\n" +
                      $"Peso: {pokemon.Peso} \n" +
                      $"URL del Sprite: {pokemon.Imagenes.SpriteFrontal}\n" +
                      $"Habilidades:\n";

        foreach (var slot in pokemon.Habilidades)
        {
            info += $"- {slot.InformacionHabilidad.NombreHabilidad}\n";
        }

        Debug.Log(info);
    }
}