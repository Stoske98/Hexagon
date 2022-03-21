using Unity.Networking.Transport;
using UnityEngine;

public class Match
{
    public int[] Accept { get; set; }
    public Entity[] Entities { get; set; }
    public NetworkConnection[] Connections { get; set; }

    public Match(NetworkConnection connection1, NetworkConnection connection2)
    {
        Accept = new int[2];
        Accept[0] = -1;
        Accept[1] = -1;

        Entities = new Entity[2];
        Connections = new NetworkConnection[2];

        Connections[0] = connection1;
        Connections[1] = connection2;

        Debug.Log("Kreiran mec");
    }

    public bool AccpetResult(int answer, NetworkConnection connection)
    {
        if(Connections[0] == connection || Connections[1] == connection)
        {
            if (Accept[0] == -1)
            {
                Accept[0] = answer;
                return false;
            }
            else if (Accept[0] == 0)
            {
                return false;
            }
            else
            {
               
                Accept[1] = answer;
                if (answer == 1)
                    return true;
                else return false;
            }
        }

        return false;
    }
}