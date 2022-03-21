using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class ServerManager : MonoBehaviour
{

    [SerializeField] ushort port;
    
    void Start()
    {
        if (!Application.isBatchMode)
            Server.Instance.Init(port);

        RegisterToEvent();
    }

    private void RegisterToEvent()
    {
        NetUtility.S_WELCOME_REQUEST += OnWelcomeRequest;
        NetUtility.S_CREATE_ACCOUNT_REQUEST += OnCreateAccountRequest;
        NetUtility.S_LOGIN_REQUEST += OnLoginRequest;
        NetUtility.S_CHAT_MESSAGE_REQUEST += OnChatMessageRequest;
        NetUtility.S_FRIEND_REQUEST_REQUEST += OnFriendRequestRequest;
        NetUtility.S_ACCEPT_FR_REQUEST += OnAcceptFriendRequestRequest;
        NetUtility.S_PLAYER_FRIEND_REQUEST += OnListPlayerFriendRequest;

        NetUtility.S_CREATE_TICKET_REQUEST += OnCreateTicketRequest;
        NetUtility.S_FIND_MATCH_REQUEST += OnFindMatchRequest;
        NetUtility.S_CANCEL_MATCH_FIDNING_REQUEST += OnCancelMatchFindingRequest;
        NetUtility.S_ACCEPT_MATCH_REQUEST += OnAcceptMatchRequest;
    }
    public void UnregisterToEvent()
    {
        NetUtility.S_WELCOME_REQUEST -= OnWelcomeRequest;
        NetUtility.S_CREATE_ACCOUNT_REQUEST -= OnCreateAccountRequest;
        NetUtility.S_LOGIN_REQUEST -= OnLoginRequest;
        NetUtility.S_CHAT_MESSAGE_REQUEST -= OnChatMessageRequest;
        NetUtility.S_FRIEND_REQUEST_REQUEST -= OnFriendRequestRequest;
        NetUtility.S_ACCEPT_FR_REQUEST -= OnAcceptFriendRequestRequest;
        NetUtility.S_PLAYER_FRIEND_REQUEST -= OnListPlayerFriendRequest;

        NetUtility.S_CREATE_TICKET_REQUEST -= OnCreateTicketRequest;
        NetUtility.S_FIND_MATCH_REQUEST -= OnFindMatchRequest;
        NetUtility.S_CANCEL_MATCH_FIDNING_REQUEST -= OnCancelMatchFindingRequest;
        NetUtility.S_ACCEPT_MATCH_REQUEST -= OnAcceptMatchRequest;
    }


    private void OnWelcomeRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.OnWelcome(message, connection);
    }

    private void OnCreateAccountRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.OnCreateAccount(message, connection);
    }

    private void OnLoginRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.OnLogin(message, connection);
    }

    private void OnChatMessageRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.ChatMessage(message, connection);
    }

    private void OnFriendRequestRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.FriendRequest(message, connection);
    }
    private void OnAcceptFriendRequestRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.AcceptFriendRequest(message, connection);
    }

    private void OnListPlayerFriendRequest(NetMessage message, NetworkConnection connection)
    {
        ServerJobSystem.ListPlayerFriends(message, connection);
    }

    private void OnCreateTicketRequest(NetMessage message, NetworkConnection connection)
    {
        Matchmaking.Instance.CreateTicketRequest(message, connection);
    }

    private void OnFindMatchRequest(NetMessage message, NetworkConnection connection)
    {
        Matchmaking.Instance.FindMatchRequest(message, connection);
    }
    private void OnCancelMatchFindingRequest(NetMessage message, NetworkConnection connection)
    {
        Matchmaking.Instance.CancelMatchFindingRequest(message, connection);
    }

    private void OnAcceptMatchRequest(NetMessage message, NetworkConnection connection)
    {
        Matchmaking.Instance.AcceptMatchRequest(message, connection);
    }
}
