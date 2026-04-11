using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SequentialSpawner : MonoBehaviourPunCallbacks
{
    [Header("Configuración de Spawneo")]
    [Tooltip("El prefab debe estar en la carpeta 'Resources'")]
    public GameObject playerPrefab;
    
    [Tooltip("Lista de puntos de aparición en orden")]
    public Transform[] spawnPoints;
    
    [Tooltip("Manager de colores")]
    public ColorsRPC colorsManager;
    
    
    // Índice interno para llevar la cuenta de cuántos jugadores han spawneado
    private int _spawnedCount = 0;

    public override void OnJoinedRoom()
    {
        // Si nosotros somos el Master Client al entrar, spawneamos nuestro propio avatar
        if (PhotonNetwork.IsMasterClient)
        {
            SpawnNewPlayer(PhotonNetwork.LocalPlayer);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        // Solo el Master Client tiene permiso para ejecutar la lógica de creación
        if (PhotonNetwork.IsMasterClient)
        {
            // Usamos un RPC para que todos sepan que se va a spawnear a alguien 
            // y así mantener sincronizado el índice de spawneo si fuera necesario,
            // pero aquí lo ejecutamos directamente en el Master.
            SpawnNewPlayer(newPlayer);
        }
    }

    private void SpawnNewPlayer(Player player)
    {
        if (_spawnedCount >= spawnPoints.Length)
        {
            Debug.LogWarning("Se han agotado los puntos de spawn definidos.");
            return;
        }

        // Seleccionamos el transform que toca
        Transform selectedPoint = spawnPoints[_spawnedCount];

        // El Master Client instancia el prefab. 
        // Nota: Por defecto, el Master Client será el 'owner'. 
        // Si quieres que el jugador que entra lo controle, usamos la sobrecarga de Photon.
        GameObject playerGO = PhotonNetwork.Instantiate(
            playerPrefab.name, 
            selectedPoint.position, 
            selectedPoint.rotation
        );

        // Transferimos el control (ownership) al jugador que acaba de entrar
        PhotonView pv = playerGO.GetComponent<PhotonView>();
        if (pv != null)
        {
            pv.TransferOwnership(player);
            
            colorsManager.AddPlayer(pv);
        }

        Debug.Log($"MasterClient creó avatar para {player.NickName} en el punto {_spawnedCount}");

        // Incrementamos el contador para el siguiente jugador
        _spawnedCount++;
    }
}