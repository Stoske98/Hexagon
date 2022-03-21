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
    }

    public override void Deserialize(DataStreamReader reader)
    {
        mineID = reader.ReadFixedString32().ToString();
        friendID = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_ACCEPT_FR_REQUEST?.Invoke(this, connection);
    }
}