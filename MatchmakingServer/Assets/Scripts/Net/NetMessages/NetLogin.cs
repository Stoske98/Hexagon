using Unity.Networking.Transport;

public class NetLogin : NetMessage
{
    public string email { set; get; }
    public string password { set; get; }

    public string nickname { set; get; }
    public int rank { set; get; }
    public string specialID { set; get; }
    public NetLogin()
    {
        Code = OpCode.LOGIN;
    }

    public NetLogin(DataStreamReader reader)
    {
        Code = OpCode.LOGIN;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteFixedString32(nickname);
        writer.WriteInt(rank);
        writer.WriteFixedString32(specialID);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        email = reader.ReadFixedString32().ToString();
        password = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_LOGIN_REQUEST?.Invoke(this, connection);
    }
}
