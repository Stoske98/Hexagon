using Unity.Networking.Transport;

public class NetAcceptFR : NetMessage
{
    public string mineID { set; get; }
    public string friendID { set; get; }
    public NetAcceptFR()
    {
        Code = OpCode.ACCEPT_FR;
    }

    public NetAcceptFR(DataStreamReader reader)
    {
        Code = OpCode.ACCEPT_FR;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteFixedString32(mineID);
        writer.WriteFixedString32(friendID);
    }

    public override void Deserialize(DataStreamReader reader)
    {
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_ACCEPT_FR_RESPONESS?.Invoke(this);
    }
}
