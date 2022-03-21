using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,

    CREATE_ACCOUNT = 101,
    LOGIN = 102,
    MESSAGE = 103,
    ERROR = 104,
    FRIEND_REQUEST = 105,
    ACCEPT_FR = 106,
    PLAYER_FRIEND = 107,

    CREATE_TICKET = 201,
    FIND_MATCH = 202,
    MATCH_FOUND = 203,
    CANCEL_MATCH_FINDING = 204,
    ACCEPT_MATCH = 205,
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
            case OpCode.WELCOME:
                msg = new NetWelcome(reader);
                break;
            case OpCode.CREATE_ACCOUNT:
                msg = new NetCreateAccount(reader);
                break;
            case OpCode.LOGIN:
                msg = new NetLogin(reader);
                break;
            case OpCode.ERROR:
                msg = new NetError(reader);
                break;
            case OpCode.MESSAGE:
                msg = new NetChatMessage(reader);
                break;
            case OpCode.FRIEND_REQUEST:
                msg = new NetFriendRequest(reader);
                break;
            case OpCode.ACCEPT_FR:
                msg = new NetAcceptFR(reader);
                break;
            case OpCode.PLAYER_FRIEND:
                msg = new NetPlayerFriend(reader);
                break;
            case OpCode.CREATE_TICKET:
                msg = new NetCreateTicket(reader);
                break;
            case OpCode.FIND_MATCH:
                msg = new NetFindMatch(reader);
                break;
            case OpCode.MATCH_FOUND:
                msg = new NetMatchFounded(reader);
                break;
            case OpCode.CANCEL_MATCH_FINDING:
                msg = new NetCancelMatchFinding(reader);
                break;
            case OpCode.ACCEPT_MATCH:
                msg = new NetAcceptMatch(reader);
                break;
            default:
                Debug.Log("Message received had no OpCode");
                break;
        }

        msg.ReceivedOnServer(connection);
    }

    // Net message
    public static Action<NetMessage, NetworkConnection> S_KEEP_ALIVE;
    public static Action<NetMessage, NetworkConnection> S_WELCOME_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_CREATE_ACCOUNT_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_LOGIN_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_ERROR_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_CHAT_MESSAGE_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_FRIEND_REQUEST_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_ACCEPT_FR_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_PLAYER_FRIEND_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_CREATE_TICKET_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_FIND_MATCH_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_MATCH_FOUND_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_CANCEL_MATCH_FIDNING_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_ACCEPT_MATCH_REQUEST;
}
