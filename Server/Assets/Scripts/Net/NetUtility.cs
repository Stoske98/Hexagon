using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    KEEP_ALIVE = 1,
}
public static class NetUtility
{
    public static void OnData(DataStreamReader reader, NetworkConnection connection, Server server = null)
    {
        NetMessage msg = null;
        // read for first byte to see what opcode is
        OpCode opCode = (OpCode)reader.ReadByte();
        switch (opCode)
        {
            case OpCode.KEEP_ALIVE:
                msg = new NetKeepAlive(reader);
                break;
            default:
                Debug.Log("Message received had no OpCode");
                break;
        }

        msg.ReceivedOnServer(connection);
    }


    // Net message
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
}
