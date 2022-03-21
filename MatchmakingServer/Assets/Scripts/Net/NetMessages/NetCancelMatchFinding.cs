using Unity.Networking.Transport;

public class NetCancelMatchFinding : NetMessage
{
    public string ticketID;
    public int Rank;
    public NetCancelMatchFinding()
    {
        Code = OpCode.CANCEL_MATCH_FINDING;
    }

    public NetCancelMatchFinding(DataStreamReader reader)
    {
        Code = OpCode.CANCEL_MATCH_FINDING;
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
        NetUtility.S_CANCEL_MATCH_FIDNING_REQUEST?.Invoke(this, connection);
    }
}
