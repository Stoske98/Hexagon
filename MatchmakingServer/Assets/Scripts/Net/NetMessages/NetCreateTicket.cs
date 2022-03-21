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
        writer.WriteFixedString32(ticketID);
    }

    public override void Deserialize(DataStreamReader reader)
    {
        createMatchmakingTicket = new CreateMatchmakingTicket
        {
            Entity = new Entity
            {
                SpecialID = reader.ReadFixedString32().ToString(),
                Nickname = reader.ReadFixedString32().ToString(),
                Rank = reader.ReadInt(),
                Class = reader.ReadInt(),
            },
            GiveUpAfterSeconds = reader.ReadInt(),
        };


    }

    public override void ReceivedOnServer(NetworkConnection connection)
    {
        NetUtility.S_CREATE_TICKET_REQUEST?.Invoke(this, connection);
    }
}