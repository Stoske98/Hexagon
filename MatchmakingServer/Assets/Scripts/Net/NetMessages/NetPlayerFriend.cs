using Unity.Networking.Transport;

public class NetPlayerFriend : NetMessage
{
    public string Nickname { set; get; }
    public string specialID { set; get; }
    public int isOnline { set; get; }
    public NetPlayerFriend()
    {
        Code = OpCode.PLAYER_FRIEND;
    }

    public NetPlayerFriend(DataStreamReader reader)
    {
        Code = OpCode.PLAYER_FRIEND;
        Deserialize(reader);

    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteFixedString32(Nickname);
        writer.WriteFixedString32(specialID);
        writer.WriteInt(isOnline);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        specialID = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_PLAYER_FRIEND_REQUEST?.Invoke(this, connection);
    }
}
