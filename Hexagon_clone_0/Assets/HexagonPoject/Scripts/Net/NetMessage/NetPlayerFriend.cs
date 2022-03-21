using Unity.Networking.Transport;

public class NetPlayerFriend : NetMessage
{
    public string Nickname { set; get; }
    public string userID { set; get; }
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
        /*writer.WriteFixedString32(Nickname);*/
        writer.WriteFixedString32(userID);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        Nickname = reader.ReadFixedString32().ToString();
        userID = reader.ReadFixedString32().ToString();
        isOnline = reader.ReadInt();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_PLAYER_FRIEND_RESPONESS?.Invoke(this);
    }
}
