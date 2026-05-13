public struct CompactStats
{
    public int Health;    // 4 bytes
    public byte Level;    // 1 byte (ahorra espacio vs int)
    public float PosX;    // 4 bytes
    
    // Total: 9 bytes por paquete
}

public class OptimizedSync : MonoBehaviourPun, IPunObservable
{
    public CompactStats stats;
    private byte[] _writeBuffer = new byte[9]; // Buffer reutilizable

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Convertimos Health a bytes en la posición 0
            System.BitConverter.TryWriteBytes(new System.Span<byte>(_writeBuffer, 0, 4), stats.Health);
            // El Level es un solo byte
            _writeBuffer[4] = stats.Level;
            // Posición X en los últimos 4 bytes
            System.BitConverter.TryWriteBytes(new System.Span<byte>(_writeBuffer, 5, 4), stats.PosX);

            stream.SendNext(_writeBuffer);
        }
        else
        {
            byte[] readBuffer = (byte[])stream.ReceiveNext();
            stats.Health = System.BitConverter.ToInt32(readBuffer, 0);
            stats.Level = readBuffer[4];
            stats.PosX = System.BitConverter.ToSingle(readBuffer, 5);
        }


        if (Mathf.Abs(lastSentHealth - stats.Health) > 1 || Time.time > nextFullSync) 
        {
            stream.SendNext(stats.Health);
            lastSentHealth = stats.Health;
            // Actualizar el tiempo para forzar un sync cada X segundos aunque no cambie nada
            nextFullSync = Time.time + 2.0f; 
        }

    }

    
}




