using Unity.Networking.Transport;

public class NetMakeMove : NetMessage
{
    public int unitColumn;
    public int unitRow;
    public int desiredColumn;
    public int desiredRow;
    public int team;
    public NetMakeMove()
    {
        Code = OpCode.MAKE_MOVE;
    }
    public NetMakeMove(DataStreamReader reader)
    {
        Code = OpCode.MAKE_MOVE;
        Deserialize(reader);
    }
    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        //writer.WriteFixedString512(Matchmaker.matchID);
        writer.WriteInt(unitColumn);
        writer.WriteInt(unitRow);
        writer.WriteInt(desiredColumn);
        writer.WriteInt(desiredRow);
        writer.WriteInt(team);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        // We already read the byte in the NetUtility:OnData
        unitColumn = reader.ReadInt();
        unitRow = reader.ReadInt();
        desiredColumn = reader.ReadInt();
        desiredRow = reader.ReadInt();
        team = reader.ReadInt();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_MAKE_MOVE_REQUEST?.Invoke(this, connection);
    }
}
