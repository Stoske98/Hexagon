using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Networking.Transport;

public static class ServerJobSystem
{
    static ServerJobSystem() { }
    public static void OnWelcome(NetMessage message, NetworkConnection connection)
    {
        Debug.Log("WELCOME ON SERVER");
        /*NetWelcome request = message as NetWelcome;
        request.AssignedTeam = ++playerCount;
        Server.Instance.SendToClient(connection, request);
        // OVDE BI TREBALO DA SE TOSUJE COIN
        if (playerCount == 1)
            Server.Instance.SendToAllClients(new NetStartGame());*/
    }

    public static void OnCreateAccount(NetMessage message, NetworkConnection connection)
    {
        NetCreateAccount request = message as NetCreateAccount;
        string email = request.email;
        string nickname = request.nickname;
        string password = request.password;
        
        string id = DbManager.Instance.CreateAccount(email, nickname, password);

        if(id != "")
        {
            request.rank = 500;
            request.specialID = id;
            Server.Instance.users.Add(new User
            {
                Nickname = request.nickname,
                Rank = request.rank,
                SpecialID = request.specialID,
                Connection = connection
            });
            Server.Instance.SendToClient(connection,request);
        }
        else
        {
            NetError responess = new NetError();
            responess.ErrorType = 1;
            Server.Instance.SendToClient(connection, responess);
        }


    }

    internal static void OnLogin(NetMessage message, NetworkConnection connection)
    {
        NetLogin request = message as NetLogin;

        List<string> payerdata = DbManager.Instance.CheckLoginAccount(request.email,request.password);
        if(payerdata!=null)
        {
            request.nickname = payerdata[0];
            request.rank = int.Parse(payerdata[1]);
            request.specialID = payerdata[2];
            Server.Instance.users.Add(new User
            {
                Nickname = request.nickname,
                Rank = request.rank,
                SpecialID = request.specialID,
                Connection = connection
            });
            Server.Instance.SendToClient(connection, request);
            UpdateFriendAndMineFriendList(connection,request.specialID);
           /* List<User> friends = new List<User>();
            friends = DbManager.Instance.GetAllFriends(request.specialID);
            if(friends != null)
            {
                foreach (User friend in friends)
                {
                    NetPlayerFriend responess = new NetPlayerFriend();
                    responess.Nickname = friend.Nickname;
                    responess.specialID = friend.SpecialID;
                    responess.isOnline = friend.isOnline;
                    Server.Instance.SendToClient(connection, responess);
                }
            }*/
            //send to all friends that is online i am online
        }
        else
        {
            NetError responess = new NetError();
            responess.ErrorType = 1;
            Server.Instance.SendToClient(connection, responess);
        }

        //else send to client that that login doesnot exist
    }

    internal static void ListPlayerFriends(NetMessage message, NetworkConnection connection)
    {
        NetPlayerFriend request = message as NetPlayerFriend;
        UpdateFriendAndMineFriendList(connection, request.specialID);
        /*List<User> friends = new List<User>();
        friends = DbManager.Instance.GetAllFriends(request.specialID);
        if (friends != null)
        {
            foreach (User friend in friends)
            {
                NetPlayerFriend responess = new NetPlayerFriend();
                responess.Nickname = friend.Nickname;
                responess.specialID = friend.SpecialID;
                responess.isOnline = friend.isOnline;
                Server.Instance.SendToClient(connection, responess);
            }
        }*/
    }

    public static void AcceptFriendRequest(NetMessage message, NetworkConnection connection)
    {
        NetAcceptFR request = message as NetAcceptFR;
        DbManager.Instance.AddFirend(request.mineID,request.friendID);
        foreach (User user in Server.Instance.users)
        {
            if(user.SpecialID == request.mineID)
            {
                Server.Instance.SendToClient(user.Connection, request);
                break;
            }
        }
        foreach (User user in Server.Instance.users)
        {
            if (user.SpecialID == request.friendID)
            {
                Server.Instance.SendToClient(user.Connection, request);
                break;
            }
        }
        Debug.Log("Clients become friends");
    }

    public static void FriendRequest(NetMessage message, NetworkConnection connection)
    {
        NetFriendRequest request = message as NetFriendRequest;
        foreach (User user in Server.Instance.users)
        {
            if(user.SpecialID == request.friendID)
            {
                Server.Instance.SendToClient(user.Connection, request);
                break;
            }
        }
    }

    public static void ChatMessage(NetMessage message, NetworkConnection connection)
    {
        NetChatMessage request = message as NetChatMessage;
        Server.Instance.SendToAllClients(request);
        Debug.Log(request.Nickname + " send message " + request.Message);
    }
    public static void UpdateFriendFriendsList(NetworkConnection connection, string id)
    {
        List<User> friends = new List<User>();
        friends = DbManager.Instance.GetAllFriends(id);
        if (friends != null)
        {
            foreach (User friend in friends)
            {
                NetPlayerFriend responess = new NetPlayerFriend();
                responess.Nickname = friend.Nickname;
                responess.specialID = friend.SpecialID;
                responess.isOnline = friend.isOnline;
                if (friend.isOnline == 1)
                {
                    UpdateMineFriendsList(friend.Connection, friend.SpecialID);
                }
            }
        }
    }
    public static void UpdateFriendAndMineFriendList(NetworkConnection connection, string id)
    {
        List<User> friends = new List<User>();
        friends = DbManager.Instance.GetAllFriends(id);
        if (friends != null)
        {
            foreach (User friend in friends)
            {
                NetPlayerFriend responess = new NetPlayerFriend();
                responess.Nickname = friend.Nickname;
                responess.specialID = friend.SpecialID;
                responess.isOnline = friend.isOnline;
                if (friend.isOnline == 1)
                {
                    UpdateMineFriendsList(friend.Connection, friend.SpecialID);
                }
                Server.Instance.SendToClient(connection, responess);
            }
        }
    }

    public static void UpdateMineFriendsList(NetworkConnection connection, string id)
    {
        List<User> friends = new List<User>();
        friends = DbManager.Instance.GetAllFriends(id);
        if (friends != null)
        {
            foreach (User friend in friends)
            {
                NetPlayerFriend responess = new NetPlayerFriend();
                responess.Nickname = friend.Nickname;
                responess.specialID = friend.SpecialID;
                responess.isOnline = friend.isOnline;
                Server.Instance.SendToClient(connection, responess);
            }
        }
    }
    public static void UpdatePlayerListFriends(NetworkConnection connection, string id, bool selfUpdat = true, bool first = true)
    {
        List<User> friends = new List<User>();
        friends = DbManager.Instance.GetAllFriends(id);
        if (friends != null)
        {
            foreach (User friend in friends)
            {
                NetPlayerFriend responess = new NetPlayerFriend();
                responess.Nickname = friend.Nickname;
                responess.specialID = friend.SpecialID;
                responess.isOnline = friend.isOnline;
                if(friend.isOnline == 1 && first)
                {
                    UpdatePlayerListFriends(friend.Connection, friend.SpecialID, true, false);
                }
                if(selfUpdat)
                    Server.Instance.SendToClient(connection, responess);
            }
        }
    }

    public static void DisconectUpdatePlayerFriendList(ref User user)
    {
        List<User> friends = new List<User>();
        friends = DbManager.Instance.GetAllFriends(user.SpecialID);
        user.SpecialID = "";
        if (friends != null)
        {
            foreach (User friend in friends)
            {
                NetPlayerFriend responess = new NetPlayerFriend();
                responess.Nickname = friend.Nickname;
                responess.specialID = friend.SpecialID;
                responess.isOnline = friend.isOnline;
                if (friend.isOnline == 1)
                {
                    UpdatePlayerListFriends(friend.Connection, friend.SpecialID, true, false);
                }
              
            }
        }
    }
}
