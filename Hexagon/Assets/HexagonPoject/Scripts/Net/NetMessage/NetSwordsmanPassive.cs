using Unity.Networking.Transport;

public class NetSwordsmanPassive : NetMessage
{
    public int Team { set; get; }
    public NetSwordsmanPassive()
    {
        Code = OpCode.SWORDSMAN_PASSIVE;
    }

    public NetSwordsmanPassive(DataStreamReader reader)
    {
        Code = OpCode.SWORDSMAN_PASSIVE;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(Team);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        Team = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_SWORDSMAN_PASSIVE_RESPONESS?.Invoke(this);
    }
}
