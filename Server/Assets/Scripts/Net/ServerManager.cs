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
        NetUtility.S_ARCHER_SPECIAL_ABILITY_REQUEST += OnArcherSpecialAbilityRequest;
        NetUtility.S_REMOVE_FIELDS_REQUEST += OnRemoveFieldsRequst;
        NetUtility.S_AKTIVATE_KING_SPECIAL_ABILITY_REQUEST += OnKingSpecialAbilityRequest;
        NetUtility.S_SWORDSMAN_PASSIVE_REQUEST += OnSwordsmanPassiveRequest;
        NetUtility.S_CHALENGE_ROYAL_COUTNER_REQUEST += OnChalengeRoyalCounterRequest;
    }
    public void UnregisterToEvent()
    {
        NetUtility.S_WELCOME_REQUEST -= OnWelcomeRequest;
        NetUtility.S_START_GAME_REQUEST -= OnStartGameRequest;
        NetUtility.S_MAKE_MOVE_REQUEST -= OnMakeMoveRequest;
        NetUtility.S_USE_TARGET_ABILITY_REQUEST -= OnUseTargetAbilityRequest;
        NetUtility.S_MAKE_TRIKSER_MOVE_REQUEST -= OnTriksterMoveRequest;
        NetUtility.S_ARCHER_SPECIAL_ABILITY_REQUEST -= OnArcherSpecialAbilityRequest;
        NetUtility.S_REMOVE_FIELDS_REQUEST -= OnRemoveFieldsRequst;
        NetUtility.S_AKTIVATE_KING_SPECIAL_ABILITY_REQUEST -= OnKingSpecialAbilityRequest;
        NetUtility.S_SWORDSMAN_PASSIVE_REQUEST -= OnSwordsmanPassiveRequest;
        NetUtility.S_CHALENGE_ROYAL_COUTNER_REQUEST -= OnChalengeRoyalCounterRequest;
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

    private void OnArcherSpecialAbilityRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.OnArcherSpecialAbilityRequest(message, connection);
    }

    private void OnRemoveFieldsRequst(NetMessage message, NetworkConnection connection)
    {
        NetRemoveFields msg = message as NetRemoveFields;
        Server.Instance.SendToAllClients(msg);
    }
    private void OnKingSpecialAbilityRequest(NetMessage message, NetworkConnection connection)
    {
        NetActivateKingSpecialAbility msg = message as NetActivateKingSpecialAbility;
        Server.Instance.SendToAllClients(msg);
    }

    private void OnSwordsmanPassiveRequest(NetMessage message, NetworkConnection connection)
    {
        NetSwordsmanPassive msg = message as NetSwordsmanPassive;
        Debug.Log("Server send swordsman passive for team: " + msg.Team);
        Server.Instance.SendToAllClients(msg);
    }

    private void OnChalengeRoyalCounterRequest(NetMessage message, NetworkConnection connection)
    {
        NetChalengeRoyalCounter msg = message as NetChalengeRoyalCounter;
        Server.Instance.SendToAllClients(msg);
    }
}
