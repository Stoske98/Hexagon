using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine;

public class Server : MonoBehaviour
{
    #region Singleton
    public static Server Instance { set; get; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public NetworkDriver driver;
    private NativeList<NetworkConnection> connections; 

    private bool isActive = false;
    private const float keepAliveTickRate = 20.0f;
    private float lastKeepAlive;
    readonly public int ServerCapacityPlayers = 64;
    public Action connectionDropped;
    public List<User> users = new List<User>();
    public void Init(ushort port)
    {
        driver = NetworkDriver.Create();
        NetworkEndPoint endPoint = NetworkEndPoint.AnyIpv4;
        endPoint.Port = port;

        if (driver.Bind(endPoint) != 0)
        {
            Debug.Log("Unable to bind on port: " + endPoint.Port);
        }
        else
        {
            driver.Listen();
            Debug.Log("Currently listening on port: " + endPoint.Port);
        }

        connections = new NativeList<NetworkConnection>(ServerCapacityPlayers, Allocator.Persistent);
        Matchmaking.Instance.SetMatchesCapacity(ServerCapacityPlayers / 2);
        isActive = true;

    }
    public void ShutDown() 
    {
        if (isActive)
        {
            driver.Dispose();
            connections.Dispose();
            isActive = false;
        }
    }
    private void OnDestroy()
    {
        ShutDown();
    }

    public void Update()
    {
        if (!isActive)
            return;

        KeepAlive(); 
        driver.ScheduleUpdate().Complete(); 

        CleanupConnections(); 
        AcceptNewConnections();
        UpdateMessagePump(); 
    }

    private void KeepAlive()
    {
        if (Time.time - lastKeepAlive > keepAliveTickRate)
        {
            lastKeepAlive = Time.time;
            SendToAllClients(new NetKeepAlive());
        }
    }

    private void CleanupConnections()
    {
        for (int i = 0; i < connections.Length; i++) 
        {
            if (!connections[i].IsCreated) 
            {
                connections.RemoveAtSwapBack(i);
                --i;
            }
        }
    }
    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while ((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
        }
    }

    private void UpdateMessagePump()
    {
        DataStreamReader reader;
        for (int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while ((cmd = driver.PopEventForConnection(connections[i], out reader)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    NetUtility.OnData(reader, connections[i], this);
                }
                else if (cmd == NetworkEvent.Type.Disconnect)
                {
                    Debug.Log("Client disconnected from server");
                    ////////////////////////////
                    User disconectedUser = null;
                    foreach (User user in users)
                    {
                        if (connections[i] == user.Connection)
                        {
                            disconectedUser = user;
                            break;
                        }
                            
                    }
                    
                    if (disconectedUser != null)
                    {
                        ServerJobSystem.DisconectUpdatePlayerFriendList(ref disconectedUser);
                        users.Remove(disconectedUser);
                    }
                    // obavesti sve njegove prijatelje da je offline 
                    /////////////////////////////
                    connections[i] = default(NetworkConnection);
                    connectionDropped?.Invoke();
                    //ShutDown();
                }
            }
        }
    }

    public void SendToClient(NetworkConnection connection, NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }

    public void SendToAllClients(NetMessage msg)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].IsCreated)
            {
                SendToClient(connections[i], msg);
            }
        }
    }

    public int isUserOnline(string specialID)
    {
        foreach (User user in users)
        {
            if (user.SpecialID == specialID)
                return 1;
        }
        return 0;
    }

    public NetworkConnection UserConnection(string specialID)
    {
        NetworkConnection connection = new NetworkConnection();
        foreach (User user in users)
        {
            if (user.SpecialID == specialID)
                return user.Connection;
        }
        return connection;
    }

}
