using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public string SpecialID { get; set; }
    public string Nickname { get; set; }
    public int Rank { get; set; }

    public int Class { get; set; }

    public Entity(string id, string name, int mmr, int team)
    {
        SpecialID = id;
        Nickname = name;
        Rank = mmr;
        Class = team;
    }

    public Entity() { }
}


