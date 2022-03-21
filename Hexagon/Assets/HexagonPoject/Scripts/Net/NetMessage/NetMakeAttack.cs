using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using Unity.Collections;

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

    public override void ReceivedOnClient()
    {
        NetUtility.C_MAKE_ATTACK_RESPONESS?.Invoke(this);
    }
}
