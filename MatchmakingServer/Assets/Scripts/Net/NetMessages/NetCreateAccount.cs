using Unity.Networking.Transport;

public class NetCreateAccount : NetMessage
{
    public string email { set; get; }
    public string nickname { set; get; }
    public string password { set; get; }

    public int rank { set; get; }

    public string specialID{ set; get; }
    public NetCreateAccount()
    {
        Code = OpCode.CREATE_ACCOUNT;
    }

    public NetCreateAccount(DataStreamReader reader)
    {
        Code = OpCode.CREATE_ACCOUNT;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteFixedString32(email);
        writer.WriteFixedString32(nickname);
        writer.WriteFixedString32(password);
        writer.WriteInt(rank);
        writer.WriteFixedString32(specialID);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        email = reader.ReadFixedString32().ToString();
        nickname = reader.ReadFixedString32().ToString();
        password = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_CREATE_ACCOUNT_REQUEST?.Invoke(this, connection);
    }
}
