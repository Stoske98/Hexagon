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
    }

    public override void Deserialize(DataStreamReader reader)
    {
        ErrorType = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_ERROR_RESPONESS?.Invoke(this);
    }
}
