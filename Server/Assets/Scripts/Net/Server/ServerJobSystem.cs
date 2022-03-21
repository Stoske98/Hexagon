using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Networking.Transport;

public static class ServerJobSystem
{
    static ServerJobSystem() { }
    public static void OnWelcomeRequsetAssignTeam(NetMessage message, NetworkConnection connection, ref int playerCount)
    {
        NetWelcome request = message as NetWelcome;
        request.AssignedTeam = ++playerCount;
        Server.Instance.SendToClient(connection, request);
        // OVDE BI TREBALO DA SE TOSUJE COIN
        if (playerCount == 1)
            Server.Instance.SendToAllClients(new NetStartGame());
    }


}
