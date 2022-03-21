using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    KEEP_ALIVE = 1,
    //GAME
    WELCOME = 2,
    START_GAME = 3,
    MAKE_MOVE = 4,
    MAKE_ATTACK = 5,
    USE_TARGET_ABILITY = 6,
    MAKE_TRIKSTER_MOVE = 7,

    CREATE_ACCOUNT = 101,
    LOGIN = 102,
    MESSAGE = 103,
    ERROR = 104,
    FRIEND_REQUEST = 105,
    ACCEPT_FR = 106,
    PLAYER_FRIEND = 107,

    //mathcmaking
    CREATE_TICKET = 201,
    FIND_MATCH = 202,
    MATCH_FOUND = 203,
    CANCEL_MATCH_FINDING = 204,
    ACCEPT_MATCH = 205,

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
            case OpCode.WELCOME:
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
                break;
            case OpCode.USE_TARGET_ABILITY:
                msg = new NetTargetAbility(reader);
                break;
            case OpCode.MAKE_TRIKSTER_MOVE:
                msg = new NetTriksterIlluMakeMove(reader);
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
                msg = new NetMatchFound(reader);
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

       
        msg.ReceivedOnClient();
    }


    // Net message
    public static Action<NetMessage> C_KEEP_ALIVE;
    public static Action<NetMessage> C_WELCOME_RESPONESS;
    public static Action<NetMessage> C_START_GAME_RESPONESS;
    public static Action<NetMessage> C_MAKE_MOVE_RESPONESS;
    public static Action<NetMessage> C_MAKE_ATTACK_RESPONESS;
    public static Action<NetMessage> C_USE_TARGET_ABILITY_RESPONESS;
    public static Action<NetMessage> C_MAKE_TRIKSTER_MOVE_RESPONESS;
    public static Action<NetMessage> C_CREATE_ACCOUNT_RESPONESS;
    public static Action<NetMessage> C_LOGIN_RESPONESS;
    public static Action<NetMessage> C_ERROR_RESPONESS;
    public static Action<NetMessage> C_CHAT_MESSAGE_RESPONESS;
    public static Action<NetMessage> C_FRIEND_REQUEST_RESPONESS;
    public static Action<NetMessage> C_ACCEPT_FR_RESPONESS;
    public static Action<NetMessage> C_PLAYER_FRIEND_RESPONESS;
    public static Action<NetMessage> C_CREATE_TICKET_RESPONESS;
    public static Action<NetMessage> C_FIND_MATCH_RESPONESS;
    public static Action<NetMessage> C_MATCH_FOUND_RESPONESS;
    public static Action<NetMessage> C_CANCEL_MATCH_FINDING_RESPONESS;
    public static Action<NetMessage> C_ACCEPT_MATCH_RESPONESS;
}
