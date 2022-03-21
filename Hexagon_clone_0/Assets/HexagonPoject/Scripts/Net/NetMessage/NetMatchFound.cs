using Unity.Networking.Transport;

public class NetMatchFound : NetMessage
{
    public int matchIndexPosition;
    public Entity entity;
    public NetMatchFound()
    {
        Code = OpCode.MATCH_FOUND;
    }

    public NetMatchFound(DataStreamReader reader)
    {
        Code = OpCode.MATCH_FOUND;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader reader)
    {

        entity = new Entity();
        matchIndexPosition = reader.ReadInt();
        entity.Nickname = reader.ReadFixedString32().ToString();
        entity.Rank = reader.ReadInt();
    }
    public override void ReceivedOnClient()
    {
        NetUtility.C_MATCH_FOUND_RESPONESS?.Invoke(this);
    }

}

