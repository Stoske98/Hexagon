using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager;
public class Matchmaking : MonoBehaviour
{
    #region Matchmaking Singleton
    private static Matchmaking _instance;

    public static Matchmaking Instance { get { return _instance; } }


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
    private string ticketID;
    private Coroutine pollTicketCoroutine;
    private int matchID;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // recieve from server
    public void CreateTicketResponess(NetMessage message)
    {
        NetCreateTicket responess = message as NetCreateTicket;
        ticketID = responess.ticketID;
        Debug.Log("Ticke ID: " + responess.ticketID);

        pollTicketCoroutine = StartCoroutine(PollTicket());

    }

    internal void FindMatchResponess(NetMessage message)
    {
        throw new NotImplementedException();
    }


    internal void FoundMatchResponess(NetMessage message)
    {
        StopCoroutine(pollTicketCoroutine);
        NetMatchFound responess = message as NetMatchFound;
        matchID = responess.matchIndexPosition;
        UIManager.Instance.enemy_nickname.text = responess.entity.Nickname;
        UIManager.Instance.enemy_rank.text = responess.entity.Rank.ToString();
        UIManager.Instance.match.SetActive(true);
        Debug.Log("Do you accept match vs " + responess.entity.Nickname + " witch rank is: " + responess.entity.Rank);
        ticketID = "";
    }

    internal void AcceptMatch(NetMessage message)
    {
        NetAcceptMatch responess = message as NetAcceptMatch;
        if(responess.Answer == 0)
        {
            // match is declined
            //UIManager.Instance.matchButton.start = !UIManager.Instance.matchButton.start;
            UIManager.Instance.accept_decline.SetActive(false);
            UIManager.Instance.match_has_declined.SetActive(true);
            UIManager.Instance.match_waiting.SetActive(false);
            //UIManager.Instance.match_finding_anim.Play("Normal");
            Invoke("HideGameObject",2f);
            Debug.Log("Match is declined");
        }
        else
        {
            //UIManager.Instance.matchButton.start = !UIManager.Instance.matchButton.start;
            UIManager.Instance.match_has_accepted.SetActive(true);
            UIManager.Instance.match_waiting.SetActive(false);
            //UIManager.Instance.match_finding_anim.Play("Normal")
            Client.Instance.ShutDown();
            //SetActive(false);
            Manager.UIManager.Instance.canvas.enabled = false;
            GameUiManager.Instance.canvas.enabled = true;
            GameManager.Instance.StartGame();
            Client.Instance.Init(ClientManager.Instance.ip, Convert.ToUInt16(responess.Port));
            Debug.Log("Both player accepted match");
        }
    }

    internal void CancelMatchFinding(NetMessage message)
    {
        StopCoroutine(pollTicketCoroutine);
        UIManager.Instance.CancelMatchFinding();
        ticketID = "";
    }
    // send to server
    public void CreateTicket(int chosenClass)
    {
        CreateMatchmakingTicket ticket = new CreateMatchmakingTicket
        {
            Entity = new Entity
            {
                SpecialID = GameManager.Instance.user_specialID,
                Nickname = GameManager.Instance.user_nickame,
                Rank = GameManager.Instance.user_rank,
                Class = chosenClass
            },

            GiveUpAfterSeconds = 120,
        };

        NetCreateTicket request = new NetCreateTicket();

        request.createMatchmakingTicket = ticket;

        Client.Instance.SendToServer(request);
    }

    public void CancelMatchFinding()
    {
        NetCancelMatchFinding request = new NetCancelMatchFinding();
        request.ticketID = ticketID;
        request.Rank = GameManager.Instance.user_rank;
        Client.Instance.SendToServer(request);
        Debug.Log("Cancel match finding requests send");

    }

    public void AccpetMatch(int answer)
    {
        NetAcceptMatch request = new NetAcceptMatch();
        request.MatchID = matchID;
        request.Answer = answer;
        Client.Instance.SendToServer(request);

        if (answer == 1)
        {
            UIManager.Instance.accept_decline.SetActive(false);
            UIManager.Instance.match_waiting.SetActive(true);
        }
        else
        {
            UIManager.Instance.matchButton.start = !UIManager.Instance.matchButton.start;
            UIManager.Instance.match_finding_anim.Play("Normal");
            UIManager.Instance.match.SetActive(false);
        }
    }

    //
    private IEnumerator PollTicket()
    {
        while (true)
        {
            NetFindMatch request = new NetFindMatch();
            request.ticketID = ticketID;
            request.Rank = GameManager.Instance.user_rank;

            Client.Instance.SendToServer(request);

            yield return new WaitForSeconds(6);
        }

    }

    public void HideGameObject()
    {
        UIManager.Instance.match.SetActive(false);
        UIManager.Instance.accept_decline.SetActive(true);
        UIManager.Instance.match_waiting.SetActive(false);

        UIManager.Instance.match_has_accepted.SetActive(false);
        UIManager.Instance.match_has_declined.SetActive(false);
    }

}
