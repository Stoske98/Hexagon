using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

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
    private Queue<Ticket>[] QueueTickets;
    private AllRanks ranks;
    private Match[] matches;
    // Start is called before the first frame update
    void Start()
    {
        ranks = new AllRanks();
        QueueTickets = new Queue<Ticket>[ranks.listOfRanks.Count];
        for (int i = 0; i < ranks.listOfRanks.Count; i++)
        {
            QueueTickets[i] = new Queue<Ticket>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateTicketRequest(NetMessage message, NetworkConnection connection)
    {
        string _ticketid = getRandomID();

        NetCreateTicket request = message as NetCreateTicket;

        CreateMatchmakingTicket ticketRequest = request.createMatchmakingTicket;

        Ticket ticket = new Ticket
        {
            TicketID = _ticketid,
            Entity = ticketRequest.Entity,
            GiveUpAfterSeconds = ticketRequest.GiveUpAfterSeconds,
            Connection = connection
        };
        int rank_position = ranks.GetQueuePosition(ticketRequest.Entity.Rank);
        QueueTickets[rank_position].Enqueue(ticket);

        NetCreateTicket responess = new NetCreateTicket();
        responess.ticketID = _ticketid;

        Server.Instance.SendToClient(connection, responess);

    }

    public void FindMatchRequest(NetMessage message, NetworkConnection connection)
    {
        int _matchID = GetFreeMatchPosision();
        NetFindMatch request = message as NetFindMatch;
        string _ticketID = request.ticketID;


        int _index = ranks.GetQueuePosition(request.Rank);
        int _rankIndex = ranks.listOfRanks.Count - 1;

        Ticket enemyTicket = null;
        Ticket mineTicket = null;
        foreach (Ticket t in QueueTickets[_index])
        {
            if (t.TicketID == _ticketID)
            {
                mineTicket = t;
                break;
            }
        }
        if (_matchID != -1)
        {
            if (QueueTickets[_index].Count != 0)
            {
                enemyTicket = QueueTickets[_index].Peek();
                if (enemyTicket != null && mineTicket != null)
                {

                    if (enemyTicket.TicketID != _ticketID && enemyTicket.Entity.Class != mineTicket.Entity.Class)
                    {
                        //mozda dodati nakon giveupaftersecond ispod 30 pokusati da nadje mec u visi ili nizi rank
                        Debug.Log("<color=red>Obrisan: </color>" + QueueTickets[_index].Peek().TicketID + "<color=green> " + QueueTickets[_index].Peek().Entity.SpecialID + " </color>");
                        QueueTickets[_index].Dequeue();
                        DeleteTicket(_ticketID.ToString(), _index);
                        Debug.Log(mineTicket.Entity.SpecialID + " vs " + enemyTicket.Entity.SpecialID);
                        SendMatchID(_matchID, enemyTicket, mineTicket.Entity, connection);
                        return;
                    }
                    else
                    {

                        enemyTicket.GiveUpAfterSeconds -= 6;
                        if (enemyTicket.GiveUpAfterSeconds <= 0)
                        {
                            QueueTickets[_index].Dequeue();
                            QueueTickets[_index].Enqueue(enemyTicket);

                        }
                        return;
                    }
                }
            }
        }

    }

    internal void AcceptMatchRequest(NetMessage message, NetworkConnection connection)
    {
        NetAcceptMatch request = message as NetAcceptMatch;
        Match match = matches[request.MatchID];
        if (match != null)
        {
            if (match.AccpetResult(request.Answer, connection))
            {
                //Match can be created
                //answer that game is ready game server ip adress and port
                string port = DbManager.Instance.EmptyPort();
                if (port != "")
                {
                    DbManager.Instance.CreateGame("192.168.0.12", port, match.Entities[0], match.Entities[1]);
                    OpenGameServerWithPort(port);
                    int p = -1;
                    int.TryParse(port, out p);
                    foreach (var cc in match.Connections)
                    {
                        NetAcceptMatch responess = new NetAcceptMatch();
                        responess.Answer = 1;
                        responess.IpAddress = "";
                        responess.Port = p;
                        Server.Instance.SendToClient(cc, responess);

                    }
                    Debug.Log("Create Match They both accept");
                }
                match = null;
            }
            if (request.Answer == 0)
            {
                //send to enemy that match is decline
                if (connection == match.Connections[0])
                {
                    NetAcceptMatch responess = new NetAcceptMatch();
                    responess.Answer = 0;
                    responess.IpAddress = "";
                    responess.Port = -1;
                    Server.Instance.SendToClient(match.Connections[1], responess);
                }
                else
                {
                    NetAcceptMatch responess = new NetAcceptMatch();
                    responess.Answer = 0;
                    responess.IpAddress = "";
                    responess.Port = -1;
                    Server.Instance.SendToClient(match.Connections[0], responess);
                }
                match = null;
            }
        }
    }

    internal void CancelMatchFindingRequest(NetMessage message, NetworkConnection connection)
    {
        NetCancelMatchFinding request = message as NetCancelMatchFinding;
        int _index = ranks.GetQueuePosition(request.Rank);
        DeleteTicket(request.ticketID, _index);

        NetCancelMatchFinding responess = new NetCancelMatchFinding();
        Server.Instance.SendToClient(connection, responess);
    }

    private void SendMatchID(int matchArrayPositon, Ticket entityWhoCreateTicket, Entity entityRequestForMatch, NetworkConnection connection)
    {
        matches[matchArrayPositon] = new Match(entityWhoCreateTicket.Connection, connection);
        matches[matchArrayPositon].Entities[0] = entityWhoCreateTicket.Entity;
        matches[matchArrayPositon].Entities[1] = entityRequestForMatch;

        NetMatchFounded responess = new NetMatchFounded();
        responess.matchIndexPosition = matchArrayPositon;
        responess.entity = entityRequestForMatch;

        Server.Instance.SendToClient(entityWhoCreateTicket.Connection, responess);

        responess.entity = entityWhoCreateTicket.Entity;
        Server.Instance.SendToClient(connection, responess);
    }
    public void DeleteTicket(string ticketID, int index)
    {
        Queue<Ticket> currentTicketQueue = new Queue<Ticket>();
        while (QueueTickets[index].Count != 0)
        {
            if (QueueTickets[index].Peek().TicketID != ticketID)
                currentTicketQueue.Enqueue(QueueTickets[index].Dequeue());
            else
            {
                Debug.Log("<color=red>Obrisan: </color>" + QueueTickets[index].Peek().TicketID + "<color=green> " + QueueTickets[index].Peek().Entity.SpecialID + " </color>");
                QueueTickets[index].Dequeue();

            }
        }

        QueueTickets[index] = currentTicketQueue;
    }
    public void SetMatchesCapacity(int capacity)
    {
        matches = new Match[capacity];
        for (int i = 0; i < capacity; i++)
        {
            matches[i] = null;
        }
    }


    public string getRandomID()
    {
        string id = string.Empty;
        for (int i = 0; i < 5; i++)
        {
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26)
                id += (char)(random + 65);
            else
                id += (random - 26).ToString();
        }

        return id;
    }

    public int GetFreeMatchPosision()
    {
        int freeIndex = -1;
        for (int i = 0; i < matches.Length; i++)
        {
            if (matches[i] == null)
            {
                freeIndex = i;
                break;
            }
        }
        return freeIndex;
    }

    public static void OpenGameServerWithPort(string port)
    {
        System.Diagnostics.Process.Start("D:/Unity/Hexagon/Server/Build/Server.exe", port);
    }

}





