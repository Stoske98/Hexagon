using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    internal static void OnArcherSpecialAbilityRequest(NetMessage message, NetworkConnection connection)
    {
        NetArcherSpecialAbility request = message as NetArcherSpecialAbility;
        if(!percentege(0.2f))
        {
            request.Archer1Column = -1;
            request.Archer1Row = -1;
        }

        if (!percentege(0.2f))
        {
            request.Archer2Column = -1;
            request.Archer2Row = -1;
        }

        Server.Instance.SendToAllClients(request);
    }

    private static bool percentege(float procChance)
    {
        float f = Random.Range(0f, 1f);
        //Debug.Log(f * 100f + "%");
        if (f < procChance)
            return true;
        return false;
    }
}
