using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    public ushort port;
    private int playerCount = -1;
    private int timeTurn = 30;
    public Text text;
    public bool isTest;
    void Start()
    {
        /* if (!Application.isBatchMode)
             Server.Instance.Init(port);

         RegisterToEvent();*/
        if(isTest)
        {
            Server.Instance.Init(port);
            RegisterToEvent();
        }
        else
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                text.text += args[i] + "\n";
            }
            //args[0] lokacija
            //args[1] port
            port = Convert.ToUInt16(args[1]);
            Server.Instance.Init(port);
            RegisterToEvent();
        }

    } 

    private void RegisterToEvent()
    {
        NetUtility.S_WELCOME_REQUEST += OnWelcomeRequest;
        NetUtility.S_START_GAME_REQUEST += OnStartGameRequest;
        NetUtility.S_MAKE_MOVE_REQUEST += OnMakeMoveRequest;
        NetUtility.S_MAKE_ATTACK_REQUEST += OnMakeAttackRequest;
        NetUtility.S_USE_TARGET_ABILITY_REQUEST += OnUseTargetAbilityRequest;
        NetUtility.S_MAKE_TRIKSER_MOVE_REQUEST += OnTriksterMoveRequest;
    }
    public void UnregisterToEvent()
    {
        NetUtility.S_WELCOME_REQUEST -= OnWelcomeRequest;
        NetUtility.S_START_GAME_REQUEST -= OnStartGameRequest;
        NetUtility.S_MAKE_MOVE_REQUEST -= OnMakeMoveRequest;
        NetUtility.S_USE_TARGET_ABILITY_REQUEST -= OnUseTargetAbilityRequest;
        NetUtility.S_MAKE_TRIKSER_MOVE_REQUEST = OnTriksterMoveRequest;
    }
       

    private void OnWelcomeRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.OnWelcomeRequsetAssignTeam(message, connection, ref playerCount);
    }
    private void OnStartGameRequest(NetMessage message, NetworkConnection connection)
    {
        //TODO kada se od klijenta startuje game, vraca tu povratnu informaciju i kada je kod obojice startovano pokrece se vreme
        throw new NotImplementedException();
    }
    private void OnMakeMoveRequest(NetMessage message, NetworkConnection connection)
    {
        NetMakeMove msg = message as NetMakeMove;
        Debug.Log("Move unit");
        Server.Instance.SendToAllClients(msg);
    }
    private void OnMakeAttackRequest(NetMessage message, NetworkConnection connection)
    {
        NetMakeAttack msg = message as NetMakeAttack;
        Debug.Log("Attack another enemy unit");
        Server.Instance.SendToAllClients(msg);
    }

    private void OnUseTargetAbilityRequest(NetMessage message, NetworkConnection connection)
    {
        NetTargetAbility msg = message as NetTargetAbility;
        Debug.Log("Send Use Target Ability");
        Server.Instance.SendToAllClients(msg);
    }

    private void OnTriksterMoveRequest(NetMessage message, NetworkConnection connection)
    {
        NetTriksterIlluMakeMove msg = message as NetTriksterIlluMakeMove;
        Debug.Log("Move Triksetr");
        Server.Instance.SendToAllClients(msg);
    }
}
