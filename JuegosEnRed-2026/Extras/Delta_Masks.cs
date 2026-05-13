using UnityEngine;
using Photon.Pun;

public class EnemyDeltaSync : MonoBehaviourPun, IPunObservable
{
    [Header("Configuración Delta")]
    public float positionThreshold = 0.1f; // Solo envía si se movió más de 10cm
    public int healthThreshold = 0;       // Envía si la vida cambia aunque sea 1 punto
    public float forceSyncInterval = 5f;  // "Heartbeat": Sincroniza todo cada 5s sí o sí

    private EnemySyncData lastSentData;
    private float lastSyncTime;

    // Datos actuales en el objeto
    public int currentHealth = 100;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 1. Verificamos si los cambios superan los umbrales (Thresholds)
            bool posChanged = Vector3.Distance(transform.position, lastSentData.Position) > positionThreshold;
            bool healthChanged = currentHealth != lastSentData.Health;
            bool forceSync = Time.time - lastSyncTime > forceSyncInterval;

            if (posChanged || healthChanged || forceSync)
            {
                // Enviamos un encabezado (Header) para que el receptor sepa qué cambió
                // Usamos un byte como máscara de bits (Bitmask) para ahorrar espacio
                byte changeMask = 0;
                if (posChanged || forceSync) changeMask |= 1; // Bit 0: Posición
                if (healthChanged || forceSync) changeMask |= 2; // Bit 1: Vida

                stream.SendNext(changeMask);

                if (posChanged || forceSync)
                {
                    stream.SendNext(transform.position);
                    lastSentData.Position = transform.position;
                }

                if (healthChanged || forceSync)
                {
                    stream.SendNext(currentHealth);
                    lastSentData.Health = currentHealth;
                }

                lastSyncTime = Time.time;
            }
        }
        else
        {
            // 2. Recepción selectiva basada en la máscara de bits
            byte changeMask = (byte)stream.ReceiveNext();

            if ((changeMask & 1) != 0) // Si el bit 0 está encendido
            {
                Vector3 networkPos = (Vector3)stream.ReceiveNext();
                // Aquí podrías usar una Corrutina o Lerp para suavizar el movimiento
                transform.position = networkPos; 
            }

            if ((changeMask & 2) != 0) // Si el bit 1 está encendido
            {
                this.currentHealth = (int)stream.ReceiveNext();
                Debug.Log($"Vida actualizada por red: {currentHealth}");
            }
        }
    }
}