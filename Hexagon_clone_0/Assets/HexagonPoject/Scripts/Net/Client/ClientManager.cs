using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClientManager : MonoBehaviour
{
    #region ClientManager Singleton
    private static ClientManager _instance;

    public static ClientManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }
    #endregion
    [SerializeField]public  string ip;
    [SerializeField] ushort port;
    public InputField ip_if;
    public InputField port_if;
    public bool isTest;
    void Start()
    {
        if(isTest)
        {
            Client.Instance.Init(ip, 27001);
            RegisterToEvent();
            Manager.UIManager.Instance.canvas.enabled = false;
            GameUiManager.Instance.canvas.enabled = true;
            GameUiManager.Instance.connectingPanel.SetActive(false);
            GameManager.Instance.StartGame();
        }
        else
        {
        Client.Instance.Init(ip,port);
        RegisterToEvent();

        }
    }

    public void Connect()
    {
        ushort p = (ushort)int.Parse(port_if.text);
        Client.Instance.Init(ip_if.text, p);
    }

    private void RegisterToEvent()
    {
        // game
        NetUtility.C_WELCOME_RESPONESS += OnWelcomeResponess;
        NetUtility.C_START_GAME_RESPONESS += OnStartGameResponess;
        NetUtility.C_MAKE_MOVE_RESPONESS += OnMakeMoveResponess;
        NetUtility.C_MAKE_ATTACK_RESPONESS += OnMakeAttakcResponess;
        NetUtility.C_USE_TARGET_ABILITY_RESPONESS += OnUseTargetAbilityResponess;
        NetUtility.C_MAKE_TRIKSTER_MOVE_RESPONESS += OnTriksetMakeMoveResponess;
        NetUtility.C_ARCHER_SPECIAL_ABILITY_RESPONESS += OnArcherSpecialAbilityResponess;
        NetUtility.C_REMOVE_FIELDS_RESPONESS += OnRemoveFieldsResponess;
        NetUtility.C_ACTIVATE_KING_SPECIAL_ABILITY += OnActivateKingSpecialAbilityResponess;
        NetUtility.C_SWORDSMAN_PASSIVE_RESPONESS += OnSwordsmanPassiveResponess;
        NetUtility.C_CHALENGE_ROYAL_COUNTER_RESPONESS += OnChalengeRoyalResponess;

        //login and stuf
        NetUtility.C_CREATE_ACCOUNT_RESPONESS += OnCreateAccountResponess;
        NetUtility.C_LOGIN_RESPONESS += OnLoginResponess;
        NetUtility.C_CHAT_MESSAGE_RESPONESS += OnChatMessageResponess;
        NetUtility.C_ERROR_RESPONESS += OnErrorResponess;
        NetUtility.C_FRIEND_REQUEST_RESPONESS += OnFriendRequestResponess;
        NetUtility.C_ACCEPT_FR_RESPONESS += OnAcceptFriendRequestResponess;
        NetUtility.C_PLAYER_FRIEND_RESPONESS += OnPlayerFriendResponess;

        //mathcmaking
        NetUtility.C_CREATE_TICKET_RESPONESS += OnCreateTicketResponess;
        NetUtility.C_FIND_MATCH_RESPONESS += OnFindMatchResponess;
        NetUtility.C_MATCH_FOUND_RESPONESS += OnMatchFoundResponess;
        NetUtility.C_CANCEL_MATCH_FINDING_RESPONESS += OnCancelMatchFindingResponess;
        NetUtility.C_ACCEPT_MATCH_RESPONESS += OnAcceptMatchResponess;


    }
    public void UnregisterToEvent()
    {
        NetUtility.C_WELCOME_RESPONESS -= OnWelcomeResponess;
        NetUtility.C_START_GAME_RESPONESS -= OnStartGameResponess;
        NetUtility.C_MAKE_MOVE_RESPONESS -= OnMakeMoveResponess;
        NetUtility.C_MAKE_ATTACK_RESPONESS -= OnMakeAttakcResponess;
        NetUtility.C_USE_TARGET_ABILITY_RESPONESS -= OnUseTargetAbilityResponess;
        NetUtility.C_MAKE_TRIKSTER_MOVE_RESPONESS -= OnTriksetMakeMoveResponess;
        NetUtility.C_ARCHER_SPECIAL_ABILITY_RESPONESS -= OnArcherSpecialAbilityResponess;
        NetUtility.C_REMOVE_FIELDS_RESPONESS -= OnRemoveFieldsResponess;
        NetUtility.C_ACTIVATE_KING_SPECIAL_ABILITY -= OnActivateKingSpecialAbilityResponess;
        NetUtility.C_SWORDSMAN_PASSIVE_RESPONESS -= OnSwordsmanPassiveResponess;
        NetUtility.C_CHALENGE_ROYAL_COUNTER_RESPONESS -= OnChalengeRoyalResponess;

        NetUtility.C_CREATE_ACCOUNT_RESPONESS -= OnCreateAccountResponess;
        NetUtility.C_LOGIN_RESPONESS -= OnLoginResponess;
        NetUtility.C_CHAT_MESSAGE_RESPONESS -= OnChatMessageResponess;
        NetUtility.C_ERROR_RESPONESS -= OnErrorResponess;
        NetUtility.C_FRIEND_REQUEST_RESPONESS -= OnFriendRequestResponess;
        NetUtility.C_ACCEPT_FR_RESPONESS -= OnAcceptFriendRequestResponess;
        NetUtility.C_PLAYER_FRIEND_RESPONESS -= OnPlayerFriendResponess;

        NetUtility.C_CREATE_TICKET_RESPONESS -= OnCreateTicketResponess;
        NetUtility.C_FIND_MATCH_RESPONESS -= OnFindMatchResponess;
        NetUtility.C_MATCH_FOUND_RESPONESS -= OnMatchFoundResponess;
        NetUtility.C_CANCEL_MATCH_FINDING_RESPONESS -= OnCancelMatchFindingResponess;
        NetUtility.C_ACCEPT_MATCH_RESPONESS -= OnAcceptMatchResponess;
    }

    private void OnWelcomeResponess(NetMessage message)
    {
        ClientJobSystem.Welcome(message);
    }
    private void OnStartGameResponess(NetMessage message)
    {
        GameManager.Instance.ChangeCamera();
    }
    private void OnMakeMoveResponess(NetMessage message)
    {
        ClientJobSystem.MoveUnit(message);
    }
    private void OnMakeAttakcResponess(NetMessage message)
    {
        ClientJobSystem.AttackUnit(message);
    }
    private void OnUseTargetAbilityResponess(NetMessage message)
    {
        ClientJobSystem.UseTargetAbility(message);
    }

    private void OnTriksetMakeMoveResponess(NetMessage message)
    {
        ClientJobSystem.MoveTrikset(message);
    }

    private void OnArcherSpecialAbilityResponess(NetMessage message)
    {
        ClientJobSystem.ArcherSpecialAbility(message);
    }

    private void OnRemoveFieldsResponess(NetMessage message)
    {
        ClientJobSystem.RemoveFields(message);
    }

    private void OnCreateAccountResponess(NetMessage message)
    {
        ClientJobSystem.LoginOnServer(message);
    }

    private void OnActivateKingSpecialAbilityResponess(NetMessage message)
    {
        ClientJobSystem.ActivateKingSpecialAbility(message);
    }

    private void OnSwordsmanPassiveResponess(NetMessage message)
    {
        ClientJobSystem.SwordsmanPassive(message);
    }

    private void OnChalengeRoyalResponess(NetMessage message)
    {
        ClientJobSystem.ChalengeRoyalCounter(message);
    }


    private void OnLoginResponess(NetMessage message)
    {
        ClientJobSystem.LoginIn(message);
    }

    private void OnErrorResponess(NetMessage message)
    {
        ClientJobSystem.Error(message);
    }

    private void OnChatMessageResponess(NetMessage message)
    {
        ClientJobSystem.ChatMessage(message);
    }

    private void OnFriendRequestResponess(NetMessage message)
    {
        ClientJobSystem.FriendRequst(message);
    }
    private void OnAcceptFriendRequestResponess(NetMessage message)
    {
        ClientJobSystem.AcceptFriendRequest(message);
    }

    private void OnPlayerFriendResponess(NetMessage message)
    {
        ClientJobSystem.PlayerFriend(message);
    }

    private void OnCreateTicketResponess(NetMessage message)
    {
        Matchmaking.Instance.CreateTicketResponess(message);
    }

    private void OnFindMatchResponess(NetMessage message)
    {
        Matchmaking.Instance.FindMatchResponess(message);
    }
    private void OnMatchFoundResponess(NetMessage message)
    {
        Matchmaking.Instance.FoundMatchResponess(message);
    }

    private void OnCancelMatchFindingResponess(NetMessage message)
    {
        Matchmaking.Instance.CancelMatchFinding(message);
    }

    private void OnAcceptMatchResponess(NetMessage message)
    {
        Matchmaking.Instance.AcceptMatch(message);
    }

}
