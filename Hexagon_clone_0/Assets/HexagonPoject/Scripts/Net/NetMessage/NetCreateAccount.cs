using Unity.Networking.Transport;

public class NetCreateAccount : NetMessage
{
    //writer.WriteFixedString32(entityID);
    public string email { set; get; }
    public string nickname { set; get; }
    public string password { set; get; }

    public int rank { set; get; }
    public string specialID { set; get; }

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
    }

    public override void Deserialize(DataStreamReader reader)
    {
        email = reader.ReadFixedString32().ToString();
        nickname = reader.ReadFixedString32().ToString();
        password = reader.ReadFixedString32().ToString();
        rank = reader.ReadInt();
        specialID = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_CREATE_ACCOUNT_RESPONESS?.Invoke(this);
    }
}
