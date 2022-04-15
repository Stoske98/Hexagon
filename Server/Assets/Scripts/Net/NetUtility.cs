using System;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public enum OpCode
{
    KEEP_ALIVE = 1,
    WELCOME = 2,
    START_GAME = 3,
    MAKE_MOVE = 4,
    MAKE_ATTACK = 5,
    USE_TARGET_ABILITY = 6,
    MAKE_TRIKSTER_MOVE = 7,
    ARCHER_SPECIAL_ABILITY = 8,
    REMOVE_FIELDS = 9,
    ACTIVATE_KING_SPECIAL_ABILITY = 10,
    SWORDSMAN_PASSIVE = 11,
    CHALENGE_ROYAL_COUNTER = 12,

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
            case OpCode.ARCHER_SPECIAL_ABILITY:
                msg = new NetArcherSpecialAbility(reader);
                break;
            case OpCode.REMOVE_FIELDS:
                msg = new NetRemoveFields(reader);
                break;
            case OpCode.ACTIVATE_KING_SPECIAL_ABILITY:
                msg = new NetActivateKingSpecialAbility(reader);
                break;
            case OpCode.SWORDSMAN_PASSIVE:
                msg = new NetSwordsmanPassive(reader);
                break;
            case OpCode.CHALENGE_ROYAL_COUNTER:
                msg = new NetChalengeRoyalCounter(reader);
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
    public static Action<NetMessage, NetworkConnection> S_START_GAME_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_MAKE_MOVE_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_MAKE_ATTACK_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_USE_TARGET_ABILITY_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_MAKE_TRIKSER_MOVE_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_ARCHER_SPECIAL_ABILITY_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_REMOVE_FIELDS_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_AKTIVATE_KING_SPECIAL_ABILITY_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_SWORDSMAN_PASSIVE_REQUEST;
    public static Action<NetMessage, NetworkConnection> S_CHALENGE_ROYAL_COUTNER_REQUEST;
}
