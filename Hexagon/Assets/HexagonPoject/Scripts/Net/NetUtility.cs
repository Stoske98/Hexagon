using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    KEEP_ALIVE = 1,
    //GAME
}
public static class NetUtility
{
    public static void OnData(DataStreamReader reader, NetworkConnection connection)
    {
        NetMessage msg = null;
        // read for first byte to see what opcode is
        OpCode opCode = (OpCode)reader.ReadByte();
        switch (opCode)
        {
            case OpCode.KEEP_ALIVE:
                msg = new NetKeepAlive(reader);
                break;
           /* case OpCode.WELCOME:
                msg = new NetWelcome(reader);
                break;
            case OpCode.START_GAME:
                msg = new NetStartGame(reader);
                break;
            case OpCode.MAKE_MOVE:
                msg = new NetMakeMove(reader);
                break;
            case OpCode.MAKE_ATTACK:
                msg = new NetMakeAttack(reader);
                break;*/
            default:
                Debug.Log("Message received had no OpCode");
                break;
        }

       
        msg.ReceivedOnClient();
    }


    // Net message
    public static Action<NetMessage> C_KEEP_ALIVE;
}
