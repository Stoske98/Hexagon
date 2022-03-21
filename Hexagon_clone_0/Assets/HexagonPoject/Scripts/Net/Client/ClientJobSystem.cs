using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public static class ClientJobSystem
{
    static ClientJobSystem() { }
    //RECIEVE FROM SERVER
    public static void Welcome(NetMessage message)
    {
        NetWelcome responess = message as NetWelcome;
        GameManager.Instance.currentTeam = responess.AssignedTeam;
    }
    public static void MoveTrikset(NetMessage message)
    {
        NetTriksterIlluMakeMove responess = message as NetTriksterIlluMakeMove;
        Hex currentHex = Map.Instance.GetHex(responess.unitColumn, responess.unitRow);
        Hex desiredHex = Map.Instance.GetHex(responess.desiredColumn, responess.desiredRow);
        if (responess.team != GameManager.Instance.currentTeam)
        {
            Unit unit = Map.Instance.GetUnit(currentHex);
            if (unit != null)
            {
                GameManager.Instance.availableMoves = unit.getAvailableMoves(currentHex);
                GameManager.Instance.specialMoves = unit.getSpecialMoves(currentHex);
                if (GameManager.Instance.specialMoves.Contains(desiredHex))
                {
                    Trikster trikster = unit as Trikster;

                    Vector2Int iluPos1 = new Vector2Int(responess.ilu1Colum, responess.ilu1Row);
                    Vector2Int iluPos2 = new Vector2Int(responess.ilu2Colum, responess.ilu2Row);
                    if (iluPos1.x != -1 && iluPos1.y != -1)
                        trikster.createIllusion(ref trikster.ilusion1, Map.Instance.GetHex(iluPos1.x, iluPos1.y));
                    if (iluPos2.x != -1 && iluPos2.y != -1)
                        trikster.createIllusion(ref trikster.ilusion2, Map.Instance.GetHex(iluPos2.x, iluPos2.y));

                    trikster.isSpecialMove = true;
                    trikster.SetPath(desiredHex);
                    GameManager.Instance.InvokeEndTurn();
                }
            }
        }
    }
    public static void MoveUnit(NetMessage message)
    {
        NetMakeMove responess = message as NetMakeMove;
        Hex currentHex = Map.Instance.GetHex(responess.unitColumn, responess.unitRow);
        Hex desiredHex = Map.Instance.GetHex(responess.desiredColumn, responess.desiredRow);
        if (responess.team != GameManager.Instance.currentTeam)
        {
            Unit unit = Map.Instance.GetUnit(currentHex);
            if (unit != null)
            {
                GameManager.Instance.availableMoves = unit.getAvailableMoves(currentHex);
                GameManager.Instance.specialMoves = unit.getSpecialMoves(currentHex);
                if (GameManager.Instance.specialMoves.Contains(desiredHex))
                {
                    unit.isSpecialMove = true;
                    unit.SetPath(desiredHex);
                    GameManager.Instance.InvokeEndTurn();
                }
                else if (GameManager.Instance.availableMoves.Contains(desiredHex))
                {
                    unit.SetPath(desiredHex);
                    GameManager.Instance.InvokeEndTurn();
                }
                
            }
        }
    }

    public static void Error(NetMessage message)
    {
        NetError responess = message as NetError;
        if(responess.ErrorType == 1)
            UIManager.Instance.sincdata_panel.SetActive(false);
        Debug.Log("Error recieve");
    }

    internal static void ChatMessage(NetMessage message)
    {
        NetChatMessage responess = message as NetChatMessage;
        UIManager.Instance.chatbox.SendMessageToMainChat(responess.Message, responess.Nickname, responess.userID);
    }

    internal static void FriendRequst(NetMessage message)
    {
        NetFriendRequest responess = message as NetFriendRequest;
        UIManager.Instance.ShowFriendRequest(responess.nickname,responess.mineID);
        Debug.Log("Requset arrive");
    }

    internal static void AcceptFriendRequest(NetMessage message)
    {
        //ADD that friend in list friends
        NetAcceptFR responess = message as NetAcceptFR;
        ListAllFriend(GameManager.Instance.user_specialID);

    }

    internal static void PlayerFriend(NetMessage message)
    {
        NetPlayerFriend responess = message as NetPlayerFriend;
        UIManager.Instance.friendBox.AddInList(responess.Nickname,responess.userID,responess.isOnline);

    }

    public static void AttackUnit(NetMessage message)
    {
        NetMakeAttack responess = message as NetMakeAttack;
        Hex currentHex = Map.Instance.GetHex(responess.unitColumn, responess.unitRow);
        Hex enemyHex = Map.Instance.GetHex(responess.enemyColumn, responess.enemyRow);

        if (responess.team != GameManager.Instance.currentTeam)
        {
            Unit unit = Map.Instance.GetUnit(currentHex);
            Unit enemyUnit = Map.Instance.GetUnit(enemyHex);
            if (unit != null && enemyUnit != null)
            {
                unit.Attack(enemyUnit);
                GameManager.Instance.InvokeEndTurn();
            }
        }
    }

    public static void UseTargetAbility(NetMessage message)
    {
        NetTargetAbility responess = message as NetTargetAbility;
        Hex currentHex = Map.Instance.GetHex(responess.unitColumn, responess.unitRow);
        Hex enemyHex = Map.Instance.GetHex(responess.enemyColumn, responess.enemyRow);
        if (responess.team != GameManager.Instance.currentTeam)
        {
            Unit unit = Map.Instance.GetUnit(currentHex);
            Unit enemyUnit = Map.Instance.GetUnit(enemyHex);
            if (unit != null && enemyUnit != null)
            {
                if (responess.keycode == 1)
                {
                    unit.ability1.UseAbility(enemyHex);
                    unit.ability1.CoolDown = unit.ability1.MaxCooldown;

                }
                if (responess.keycode == 2)
                {
                    unit.ability2.UseAbility(enemyHex);
                    unit.ability2.CoolDown = unit.ability2.MaxCooldown;
                }
                /*if (responess.keycode == 3)
                    unit.ability3.UseAbility(enemyHex);*/
                GameManager.Instance.InvokeEndTurn();
            }
        }
    }

    public static void LoginOnServer(NetMessage message)
    {
        NetCreateAccount responess = message as NetCreateAccount;
        Debug.Log(responess.nickname + "\n" + responess.rank + "\n" + responess.specialID);
        GameManager.Instance.user_nickame = responess.nickname;
        GameManager.Instance.user_rank = responess.rank;
        GameManager.Instance.user_specialID = responess.specialID;
        GameManager.Instance.selected_class = 0;
        //ispisi kreiran account
        UIManager.Instance.sincdata_panel.SetActive(false);
        UIManager.Instance.main_menu.SetActive(true);
        Debug.Log("Account created");
    }

    public static void LoginIn(NetMessage message)
    {
        NetLogin responess = message as NetLogin;
        Debug.Log(responess.nickname + "\n" + responess.rank + "\n" + responess.specialID);
        GameManager.Instance.user_nickame = responess.nickname;
        GameManager.Instance.user_rank = responess.rank;
        GameManager.Instance.user_specialID = responess.specialID;
        //ispisi kreiran account
        UIManager.Instance.sincdata_panel.SetActive(false);
        UIManager.Instance.main_menu.SetActive(true);
        Debug.Log("Account Exist");
    }

    // Message to server OnMove
    public static void MoveUnitRequest(Hex from, Hex to, int team)
    {
        NetMakeMove request = new NetMakeMove();
        request.unitColumn = from.Column;
        request.unitRow = from.Row;
        request.desiredColumn = to.Column;
        request.desiredRow = to.Row;
        request.team = team;
        Client.Instance.SendToServer(request);
    }
    public static void AttackUnitRequst(Unit unit, Unit enemyUnitm, int team)
    {
        NetMakeAttack request = new NetMakeAttack();
        request.unitColumn = unit.Column;
        request.unitRow = unit.Row;
        request.enemyColumn = enemyUnitm.Column;
        request.enemyRow = enemyUnitm.Row;
        request.team = team;
        Client.Instance.SendToServer(request);
    }
    public static void UseTargetAbility(Unit unit, Unit enemyUnitm, int team, int KeyCode)
    {
        NetTargetAbility request = new NetTargetAbility();
        request.unitColumn = unit.Column;
        request.unitRow = unit.Row;
        request.enemyColumn = enemyUnitm.Column;
        request.enemyRow = enemyUnitm.Row;
        request.team = team;
        request.keycode = KeyCode;
        Client.Instance.SendToServer(request);
    }

    public static void MoveTrikserRequest(Hex from, Hex to, int team, Vector2Int iluPos1, Vector2Int iluPos2)
    {
        NetTriksterIlluMakeMove request = new NetTriksterIlluMakeMove();
        request.unitColumn = from.Column;
        request.unitRow = from.Row;
        request.desiredColumn = to.Column;
        request.desiredRow = to.Row;

        request.ilu1Colum = iluPos1.x;
        request.ilu1Row = iluPos1.y;

        request.ilu2Colum = iluPos2.x;
        request.ilu2Row = iluPos2.y;

        request.team = team;
        Client.Instance.SendToServer(request);
        Debug.Log("trikseter movement sent on Server");
    }

    public static void CreateAccountRequest(string email, string nickname, string password)
    {
        NetCreateAccount request = new NetCreateAccount();

        request.email = email;
        request.nickname = nickname;
        request.password = password;

        Client.Instance.SendToServer(request);
        Debug.Log("Send Create account request");
    }

    public static void LoginRequest(string email, string password)
    {
        NetLogin request = new NetLogin();

        request.email = email;
        request.password = password;

        Client.Instance.SendToServer(request);
        Debug.Log("Send Login request");
    }

    public static void ChatMessageRequest(string nickname, string message,string userID)
    {
        NetChatMessage request = new NetChatMessage();
        request.Nickname = nickname;
        request.Message = message;
        request.userID = userID;
        Client.Instance.SendToServer(request);
        Debug.Log("Message sended on server");
    }

    public static void FriendRequst(string nickname, string mineID, string friendID)
    {
        NetFriendRequest request = new NetFriendRequest();
        request.nickname = nickname;
        request.mineID = mineID;
        request.friendID = friendID;
        Client.Instance.SendToServer(request);
        Debug.Log("Friend request sended on server");
    }

    public static void AcceptFriendRequest(string mineID, string friendID)
    {
        NetAcceptFR request = new NetAcceptFR();
        request.mineID = mineID;
        request.friendID = friendID;
        Client.Instance.SendToServer(request);
        Debug.Log("Friend request accepted");
    }
    public static void ListAllFriend(string userID)
    {
        NetPlayerFriend request = new NetPlayerFriend();
        request.userID = userID;
        Client.Instance.SendToServer(request);

    }
}
