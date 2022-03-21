using Unity.Networking.Transport;

public class Ticket
{
    public string TicketID { get; set; }
    public Entity Entity { get; set; }
    public NetworkConnection Connection { get; set; }

    public int GiveUpAfterSeconds { get; set; }

    public Ticket(string ticketID, Entity entity, int giveUpAfterSeconds, NetworkConnection connection)
    {
        TicketID = ticketID;
        Entity = entity;
        GiveUpAfterSeconds = giveUpAfterSeconds;
        Connection = connection;
    }

    public Ticket() { }


}


