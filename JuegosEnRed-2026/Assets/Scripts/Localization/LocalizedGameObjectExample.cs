using UnityEngine;
using UnityEngine.Localization;

public class LocalizedGameObjectExample : MonoBehaviour
{
    [Header("Configuración de Localización")]
    [Tooltip("Referencia al Asset localizado (Prefab) que cambiará según el idioma.")]
    [SerializeField] private LocalizedAsset<GameObject> localizedAsset;

    [Header("Ajustes de Instanciación")]
    [Tooltip("Contenedor opcional donde se instanciará el GameObject. Si se deja vacío, se usará este mismo transform.")]
    [SerializeField] private Transform spawnParent;

    private GameObject currentInstance;

    private void OnEnable()
    {
        // Nos suscribimos al evento AssetChanged. 
        // Nota: Al suscribirse, el sistema invoca automáticamente este método con el asset actual si ya está listo.
        localizedAsset.AssetChanged += OnAssetChanged;
    }

    private void OnDisable()
    {
        // Desuscribimos el evento para evitar fugas de memoria o llamadas en objetos destruidos
        localizedAsset.AssetChanged -= OnAssetChanged;
        
        // Limpiamos la instancia actual
        ClearCurrentInstance();
    }

    /// <summary>
    /// Método que se ejecuta automáticamente cuando el sistema carga el prefab correspondiente al idioma activo.
    /// </summary>
    /// <param name="loadedAsset">El GameObject/Prefab devuelto por la Asset Table.</param>
    private void OnAssetChanged(GameObject loadedAsset)
    {
        // Destruimos la versión anterior si existía
        ClearCurrentInstance();

        if (loadedAsset != null)
        {
            Transform parentToUse = spawnParent != null ? spawnParent : transform;
            
            // Instanciamos el nuevo prefab localizado como hijo del contenedor
            currentInstance = Instantiate(loadedAsset, parentToUse);
            
            // Hacemos que se expanda para llenar todo el contenedor padre
            ExpandToFillParent(currentInstance);
        }
        else
        {
            Debug.LogWarning($"El Asset localizado en {gameObject.name} es nulo para el idioma actual.", this);
        }
    }
    /// <summary>
    /// Configura el RectTransform del objeto instanciado para que ocupe todo el espacio del padre.
    /// </summary>
    /// <param name="instance">El GameObject recién instanciado.</param>
    private void ExpandToFillParent(GameObject instance)
    {
        if (instance.TryGetComponent<RectTransform>(out RectTransform rectTransform))
        {
            // Establecemos las anclas en las esquinas extremas (inferior-izquierda y superior-derecha)
            rectTransform.anchorMin = Vector2.zero; // (0, 0)
            rectTransform.anchorMax = Vector2.one;  // (1, 1)

            // Ponemos los márgenes (offsets) a cero para que no haya separación respecto a los bordes del padre
            rectTransform.offsetMin = Vector2.zero; // Izquierda y Abajo
            rectTransform.offsetMax = Vector2.zero; // Derecha y Arriba

            // Nos aseguramos de que la escala local se mantenga normalizada
            rectTransform.localScale = Vector3.one;
        }
        else
        {
            Debug.LogWarning($"El prefab localizado en {gameObject.name} no tiene un componente RectTransform, por lo que no se pudo expandir.", instance);
        }
    }

    private void ClearCurrentInstance()
    {
        if (currentInstance != null)
        {
            Destroy(currentInstance);
            currentInstance = null;
        }
    }
}