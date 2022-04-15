using Unity.Networking.Transport;

public class NetRemoveFields : NetMessage
{
    public NetRemoveFields()
    {
        Code = OpCode.REMOVE_FIELDS;
    }

    public NetRemoveFields(DataStreamReader reader)
    {
        Code = OpCode.REMOVE_FIELDS;
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
        NetUtility.S_REMOVE_FIELDS_REQUEST?.Invoke(this, connection);
    }
}