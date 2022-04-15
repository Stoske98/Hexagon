using Unity.Networking.Transport;

public class NetActivateKingSpecialAbility : NetMessage
{
    public int onMove { set; get; }
    public NetActivateKingSpecialAbility()
    {
        Code = OpCode.ACTIVATE_KING_SPECIAL_ABILITY;
    }

    public NetActivateKingSpecialAbility(DataStreamReader reader)
    {
        Code = OpCode.ACTIVATE_KING_SPECIAL_ABILITY;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteInt(onMove);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        onMove = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_ACTIVATE_KING_SPECIAL_ABILITY?.Invoke(this);
    }
}