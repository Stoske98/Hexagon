using Unity.Networking.Transport;

public class NetFriendRequest : NetMessage
{
    public string nickname { set; get; }
    public string mineID { set; get; }
    public string friendID { set; get; }
    public NetFriendRequest()
    {
        Code = OpCode.FRIEND_REQUEST;
    }

    public NetFriendRequest(DataStreamReader reader)
    {
        Code = OpCode.FRIEND_REQUEST;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteFixedString32(nickname);
        writer.WriteFixedString32(mineID);
        writer.WriteFixedString32(friendID);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        nickname = reader.ReadFixedString32().ToString();
        mineID = reader.ReadFixedString32().ToString();
        friendID = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_FRIEND_REQUEST_REQUEST?.Invoke(this, connection);
    }
}
