using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] string ip;
    [SerializeField] ushort port;

    void Start()
    {
       // Client.Instance.Init(ip,port);

        RegisterToEvent();
    }

    private void RegisterToEvent()
    { 
    }
    public void UnregisterToEvent()
    {
    }
    
}
