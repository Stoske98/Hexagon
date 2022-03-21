using Unity.Networking.Transport;

public class NetChatMessage : NetMessage
{
    public string Nickname { set; get; }
    public string Message { set; get; }
    public string userID { set; get; }
    public NetChatMessage()
    {
        Code = OpCode.MESSAGE;
    }

    public NetChatMessage(DataStreamReader reader)
    {
        Code = OpCode.MESSAGE;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteFixedString32(Nickname);
        writer.WriteFixedString64(Message);
        writer.WriteFixedString32(userID);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        Nickname = reader.ReadFixedString32().ToString();
        Message = reader.ReadFixedString64().ToString();
        userID = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_CHAT_MESSAGE_REQUEST?.Invoke(this, connection);
    }
}
