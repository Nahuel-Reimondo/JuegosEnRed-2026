using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class LocalizedTextExample : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Referencia al componente TextMeshPro donde se mostrará el texto.")]
    [SerializeField] private TMP_Text textComponent;

    [Header("Configuración de Localización")]
    [Tooltip("Objeto que permite seleccionar la Tabla y la Clave (Key) desde el Inspector.")]
    [SerializeField] private LocalizedString localizedString;

    private void OnEnable()
    {
        // Nos suscribimos al evento StringChanged. 
        // Este evento se dispara automáticamente al inicializarse y cada vez que el usuario cambia de idioma.
        localizedString.StringChanged += UpdateText;
    }

    private void OnDisable()
    {
        // Es una buena práctica desuscribirse al deshabilitar el objeto para evitar fugas de memoria.
        localizedString.StringChanged -= UpdateText;
    }

    /// <summary>
    /// Método que se ejecuta de forma automática al cambiar el texto localizado.
    /// </summary>
    /// <param name="translatedText">El texto traducido devuelto por el sistema.</param>
    private void UpdateText(string translatedText)
    {
        if (textComponent != null)
        {
            textComponent.text = translatedText;
        }
        else
        {
            Debug.LogWarning($"Falta asignar el componente TMP_Text en {gameObject.name}", this);
        }
    }
}