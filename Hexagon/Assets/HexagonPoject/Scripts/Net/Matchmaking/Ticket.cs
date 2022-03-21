using Unity.Networking.Transport;

public class Ticket
{
    public string TicketID { get; set; }
    public string EntityID { get; set; }
    public NetworkConnection Connection { get; set; }

    public int GiveUpAfterSeconds { get; set; }

    public Ticket(string ticketID, string entityID, int giveUpAfterSeconds, NetworkConnection connection)
    {
        TicketID = ticketID;
        EntityID = entityID;
        GiveUpAfterSeconds = giveUpAfterSeconds;
        Connection = connection;
    }

    public Ticket() { }


}


