using ExitGames.Client.Photon; // Necesario para la serialización
using Photon.Pun;

public class NetworkManager : MonoBehaviour
{
    private void Awake()
    {
        // Registramos el tipo personalizado
        PhotonPeer.RegisterType(typeof(PlayerStats), (byte)'S', SerializePlayerStats, DeserializePlayerStats);
    }

    // Convierte el objeto a un array de bytes
    private static short SerializePlayerStats(StreamBuffer outStream, object customobject)
    {
        PlayerStats stats = (PlayerStats)customobject;
        byte[] bytes = new byte[4 + 4 + stats.PlayerName.Length + 1]; // Tamaño aproximado
        
        lock (bytes)
        {
            int index = 0;
            Protocol.Serialize(stats.Health, bytes, ref index);
            Protocol.Serialize(stats.Experience, bytes, ref index);
            Protocol.Serialize(stats.PlayerName, bytes, ref index);
            outStream.Write(bytes, 0, bytes.Length);
        }
        return (short)bytes.Length;
    }

    // Convierte los bytes de vuelta al objeto
    private static object DeserializePlayerStats(StreamBuffer inStream, short length)
    {
        PlayerStats stats = new PlayerStats();
        byte[] bytes = new byte[length];
        inStream.Read(bytes, 0, length);
        
        int index = 0;
        Protocol.Deserialize(out stats.Health, bytes, ref index);
        Protocol.Deserialize(out stats.Experience, bytes, ref index);
        Protocol.Deserialize(out stats.PlayerName, bytes, ref index);
        
        return stats;
    }
}

public class PlayerSync : MonoBehaviourPun, IPunObservable
{
    public PlayerStats myStats = new PlayerStats();

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Enviamos nuestra clase compleja directamente
            stream.SendNext(myStats);
        }
        else
        {
            // Recibimos y casteamos
            this.myStats = (PlayerStats)stream.ReceiveNext();
        }
    }
}