using Unity.Networking.Transport;

public class NetTargetAbility : NetMessage
{
    public int unitColumn;
    public int unitRow;
    public int enemyColumn;
    public int enemyRow;
    public int team;
    public int keycode;
    public NetTargetAbility()
    {
        Code = OpCode.USE_TARGET_ABILITY;
    }
    public NetTargetAbility(DataStreamReader reader)
    {
        Code = OpCode.USE_TARGET_ABILITY;
        Deserialize(reader);
    }
    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(unitColumn);
        writer.WriteInt(unitRow);
        writer.WriteInt(enemyColumn);
        writer.WriteInt(enemyRow);
        writer.WriteInt(team);
        writer.WriteInt(keycode);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        unitColumn = reader.ReadInt();
        unitRow = reader.ReadInt();
        enemyColumn = reader.ReadInt();
        enemyRow = reader.ReadInt();
        team = reader.ReadInt();
        keycode = reader.ReadInt();
    }
    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_USE_TARGET_ABILITY_REQUEST?.Invoke(this, connection);
    }
}

