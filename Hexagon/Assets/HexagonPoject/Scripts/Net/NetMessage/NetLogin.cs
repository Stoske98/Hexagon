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
        writer.WriteFixedString32(email);
        writer.WriteFixedString32(password);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        nickname = reader.ReadFixedString32().ToString();
        rank = reader.ReadInt();
        specialID = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_LOGIN_RESPONESS?.Invoke(this);
    }
}
