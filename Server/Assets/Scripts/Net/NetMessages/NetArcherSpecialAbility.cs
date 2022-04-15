using Unity.Networking.Transport;

public class NetArcherSpecialAbility : NetMessage
{
    public int Archer1Column { set; get; }
    public int Archer1Row { set; get; }
    public int Archer2Column { set; get; }
    public int Archer2Row { set; get; }
    public NetArcherSpecialAbility()
    {
        Code = OpCode.ARCHER_SPECIAL_ABILITY;
    }

    public NetArcherSpecialAbility(DataStreamReader reader)
    {
        Code = OpCode.ARCHER_SPECIAL_ABILITY;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(Archer1Column);
        writer.WriteInt(Archer1Row);
        writer.WriteInt(Archer2Column);
        writer.WriteInt(Archer2Row);

    }

    public override void Deserialize(DataStreamReader reader)
    {
        Archer1Column = reader.ReadInt();
        Archer1Row = reader.ReadInt();
        Archer2Column = reader.ReadInt();
        Archer2Row = reader.ReadInt();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_ARCHER_SPECIAL_ABILITY_REQUEST?.Invoke(this, connection);
    }
}
