using Unity.Networking.Transport;

public class NetFindMatch : NetMessage
{
    public string ticketID;
    public int Rank;
    public NetFindMatch()
    {
        Code = OpCode.FIND_MATCH;
    }

    public NetFindMatch(DataStreamReader reader)
    {
        Code = OpCode.FIND_MATCH;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        ticketID = reader.ReadFixedString32().ToString();
        Rank = reader.ReadInt();
    }
    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_FIND_MATCH_REQUEST?.Invoke(this, connection);
    }
}
