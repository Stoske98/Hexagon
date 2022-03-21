using System.Collections.Generic;

public class CreateMatchmakingTicket
{
    public Entity Entity { get; set; }

    public int GiveUpAfterSeconds { get; set; }


    public CreateMatchmakingTicket(Entity entity, int giveUpAfterSeconds)
    {
        Entity = entity;
        GiveUpAfterSeconds = giveUpAfterSeconds;
    }

    public CreateMatchmakingTicket() { }


}

