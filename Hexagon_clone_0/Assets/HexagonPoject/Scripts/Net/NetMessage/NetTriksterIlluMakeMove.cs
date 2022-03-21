using Unity.Networking.Transport;

public class NetTriksterIlluMakeMove : NetMessage
{
    public int unitColumn;
    public int unitRow;
    public int desiredColumn;
    public int desiredRow;
    public int ilu1Colum;
    public int ilu1Row;
    public int ilu2Colum;
    public int ilu2Row;
    public int team;
    public NetTriksterIlluMakeMove()
    {
        Code = OpCode.MAKE_TRIKSTER_MOVE;
    }
    public NetTriksterIlluMakeMove(DataStreamReader reader)
    {
        Code = OpCode.MAKE_TRIKSTER_MOVE;
        Deserialize(reader);
    }
    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(unitColumn);
        writer.WriteInt(unitRow);
        writer.WriteInt(desiredColumn);
        writer.WriteInt(desiredRow);

        writer.WriteInt(ilu1Colum);
        writer.WriteInt(ilu1Row);
        writer.WriteInt(ilu2Colum);
        writer.WriteInt(ilu2Row);

        writer.WriteInt(team);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        // We already read the byte in the NetUtility:OnData
        unitColumn = reader.ReadInt();
        unitRow = reader.ReadInt();
        desiredColumn = reader.ReadInt();
        desiredRow = reader.ReadInt();

        ilu1Colum = reader.ReadInt();
        ilu1Row = reader.ReadInt();
        ilu2Colum = reader.ReadInt();
        ilu2Row = reader.ReadInt();

        team = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_MAKE_TRIKSTER_MOVE_RESPONESS?.Invoke(this);
    }
}
