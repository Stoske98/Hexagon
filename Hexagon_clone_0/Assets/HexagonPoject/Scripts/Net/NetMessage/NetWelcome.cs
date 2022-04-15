using Unity.Networking.Transport;

public class NetWelcome : NetMessage
{
    public int AssignedTeam { set; get; }
    public NetWelcome()
    {
        Code = OpCode.WELCOME;
    }

    public NetWelcome(DataStreamReader reader)
    {
        Code = OpCode.WELCOME;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(AssignedTeam);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        AssignedTeam = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_WELCOME_RESPONESS?.Invoke(this);
    }
}
//NetChalengeRoyalCounter

public class NetChalengeRoyalCounter : NetMessage
{
    public NetChalengeRoyalCounter()
    {
        Code = OpCode.CHALENGE_ROYAL_COUNTER;
    }

    public NetChalengeRoyalCounter(DataStreamReader reader)
    {
        Code = OpCode.CHALENGE_ROYAL_COUNTER;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }

    public override void Deserialize(DataStreamReader reader)
    {
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_CHALENGE_ROYAL_COUNTER_RESPONESS?.Invoke(this);
    }
}