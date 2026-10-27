using System;
using UnityEngine;

[RequireComponent(typeof(PhotonManagerLayersExample))]
public class NetworkManager : MonoBehaviour, INetworkMediator
{
    public event Action OnConnected;
    public event Action OnJoinedRoom;
    public event Action<string> OnJoinFailed;

    private INetworkProvider _networkProvider;

    private void Awake()
    {
        _networkProvider = GetComponent<INetworkProvider>();

        // Suscripción a los eventos del proveedor de Photon
        _networkProvider.OnConnectedToMasterEvent += () => OnConnected?.Invoke();
        _networkProvider.OnJoinedRoomEvent += () => OnJoinedRoom?.Invoke();
        _networkProvider.OnJoinRoomFailedEvent += (msg) => OnJoinFailed?.Invoke(msg);
    }

    public void Connect()
    {
        _networkProvider.ConnectUsingSettings();
    }

    public void FindOrCreateMatch()
    {
        _networkProvider.JoinRandomOrCreateRoom();
    }

    public void DisconnectAndLeave()
    {
        _networkProvider.LeaveRoom();
    }
}