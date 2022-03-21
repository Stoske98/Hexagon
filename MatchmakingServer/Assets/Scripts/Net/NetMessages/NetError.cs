using Unity.Networking.Transport;

public class NetError : NetMessage
{
    public int ErrorType { set; get; }
    public NetError()
    {
        Code = OpCode.ERROR;
    }

    public NetError(DataStreamReader reader)
    {
        Code = OpCode.ERROR;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(ErrorType);
    }

    public override void Deserialize(DataStreamReader reader)
    {
      // AssignedTeam = reader.ReadInt();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_ERROR_REQUEST?.Invoke(this, connection);
    }
}
