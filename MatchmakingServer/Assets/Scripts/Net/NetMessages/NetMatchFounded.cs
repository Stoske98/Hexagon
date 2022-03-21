using Unity.Networking.Transport;

public class NetMatchFounded : NetMessage
{
    public int matchIndexPosition;
    public Entity entity;
    public NetMatchFounded()
    {
        Code = OpCode.MATCH_FOUND;
    }

    public NetMatchFounded(DataStreamReader reader)
    {
        Code = OpCode.MATCH_FOUND;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(matchIndexPosition);
        //writer.WriteFixedString32(enemyEntity.SpecialID);
        writer.WriteFixedString32(entity.Nickname);
        writer.WriteInt(entity.Rank);
    }

    public override void Deserialize(DataStreamReader reader)
    {

    }
    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_MATCH_FOUND_REQUEST?.Invoke(this, connection);
    }
}
