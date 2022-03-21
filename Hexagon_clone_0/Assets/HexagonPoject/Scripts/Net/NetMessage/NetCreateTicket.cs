using Unity.Networking.Transport;

public class NetCreateTicket : NetMessage
{
    public CreateMatchmakingTicket createMatchmakingTicket;
    public string ticketID;
    public NetCreateTicket()
    {
        Code = OpCode.CREATE_TICKET;
    }

    public NetCreateTicket(DataStreamReader reader)
    {
        Code = OpCode.CREATE_TICKET;
        Deserialize(reader);
    }

    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
        writer.WriteFixedString32(createMatchmakingTicket.Entity.SpecialID);
        writer.WriteFixedString32(createMatchmakingTicket.Entity.Nickname);
        writer.WriteInt(createMatchmakingTicket.Entity.Rank);
        writer.WriteInt(createMatchmakingTicket.Entity.Class);
        writer.WriteInt(createMatchmakingTicket.GiveUpAfterSeconds);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        ticketID = reader.ReadFixedString32().ToString();
    }

    public override void ReceivedOnClient()
    {
        NetUtility.C_CREATE_TICKET_RESPONESS?.Invoke(this);
    }
}
