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
        writer.WriteInt(Answer);
        writer.WriteFixedString32(IpAddress);
        writer.WriteInt(Port);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        MatchID = reader.ReadInt();
        Answer = reader.ReadInt();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_ACCEPT_MATCH_REQUEST?.Invoke(this, connection);
    }
}
