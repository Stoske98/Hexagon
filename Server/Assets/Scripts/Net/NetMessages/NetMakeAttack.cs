using Unity.Networking.Transport;

public class NetMakeAttack : NetMessage
{
    public int unitColumn;
    public int unitRow;
    public int enemyColumn;
    public int enemyRow;
    public int team;
    public NetMakeAttack()
    {
        Code = OpCode.MAKE_ATTACK;
    }
    public NetMakeAttack(DataStreamReader reader)
    {
        Code = OpCode.MAKE_ATTACK;
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
    }

    public override void Deserialize(DataStreamReader reader)
    {
        unitColumn = reader.ReadInt();
        unitRow = reader.ReadInt();
        enemyColumn = reader.ReadInt();
        enemyRow = reader.ReadInt();
        team = reader.ReadInt();
    }
    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_MAKE_ATTACK_REQUEST?.Invoke(this, connection);
    }
}

