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
        writer.WriteFixedString32(ticketID);
        writer.WriteInt(Rank);
    }

    public override void Deserialize(DataStreamReader reader)
    {

    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_CANCEL_MATCH_FINDING_RESPONESS?.Invoke(this);
    }

}

