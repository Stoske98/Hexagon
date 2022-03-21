using Unity.Networking.Transport;

public class NetAcceptMatch : NetMessage
{
    public int MatchID;
    public int Answer; // 0 decline, 1 accept

    public string IpAddress;
    public int Port;
    public NetAcceptMatch()
    {
        Code = OpCode.ACCEPT_MATCH;
    }

    public NetAcceptMatch(DataStreamReader reader)
    {
        Code = OpCode.ACCEPT_MATCH;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(MatchID);
        writer.WriteInt(Answer);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        Answer = reader.ReadInt();
        IpAddress = reader.ReadFixedString32().ToString();
        Port = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_ACCEPT_MATCH_RESPONESS?.Invoke(this);
    }

}

