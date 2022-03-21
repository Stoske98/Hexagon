using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class User 
{
    public string Nickname { set; get; }
    public int Rank { set; get; }
    public string SpecialID { set; get; }
    public NetworkConnection Connection { set; get; }
    public int isOnline { set; get; }
}
