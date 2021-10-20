using System.Collections;
using System.Collections.Generic;
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
    }
}
