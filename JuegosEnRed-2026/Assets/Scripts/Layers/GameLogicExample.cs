using UnityEngine;

public class GameLogicExample : MonoBehaviour
{
    [SerializeField] private MonoBehaviour networkManagerReference; // Asignar el NetworkManager desde el Inspector
    private INetworkMediator _networkMediator;

    private void Awake()
    {
        _networkMediator = networkManagerReference as INetworkMediator;

        if (_networkMediator == null)
        {
            Debug.LogError("El objeto asignado no implementa INetworkMediator.");
        }
    }

    private void OnEnable()
    {
        if (_networkMediator != null)
        {
            _networkMediator.OnConnected += HandleConnected;
            _networkMediator.OnJoinedRoom += HandleJoinedRoom;
            _networkMediator.OnJoinFailed += HandleJoinFailed;
        }
    }

    private void OnDisable()
    {
        if (_networkMediator != null)
        {
            _networkMediator.OnConnected -= HandleConnected;
            _networkMediator.OnJoinedRoom -= HandleJoinedRoom;
            _networkMediator.OnJoinFailed -= HandleJoinFailed;
        }
    }

    private void Start()
    {
        // La lógica de juego solo interactúa con el mediador desacoplado
        _networkMediator?.Connect();
    }

    private void HandleConnected()
    {
        Debug.Log("GameLogic: Conectado al servidor de red exitosamente.");
        _networkMediator.FindOrCreateMatch();
    }

    private void HandleJoinedRoom()
    {
        Debug.Log("GameLogic: ¡Sala unida con éxito! Iniciando juego...");
    }

    private void HandleJoinFailed(string error)
    {
        Debug.LogWarning($"GameLogic: Fallo al unirse a la sala: {error}");
    }
}