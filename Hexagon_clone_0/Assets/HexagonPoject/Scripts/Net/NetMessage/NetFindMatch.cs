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
        writer.WriteFixedString32(ticketID);
        writer.WriteInt(Rank);
    }

    public override void Deserialize(DataStreamReader reader)
    {

    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_FIND_MATCH_RESPONESS?.Invoke(this);
    }

}

