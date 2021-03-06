using Unity.Networking.Transport;
using Unity.Collections;
using System.Text;

public class NetKeepAlive : NetMessage
{
    public NetKeepAlive() 
    {
        Code = OpCode.KEEP_ALIVE;
    }

    public NetKeepAlive(DataStreamReader reader)
    {
        Code = OpCode.KEEP_ALIVE;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader reader)
    {

    }
    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_KEEP_ALIVE?.Invoke(this, connection);
    }
}
