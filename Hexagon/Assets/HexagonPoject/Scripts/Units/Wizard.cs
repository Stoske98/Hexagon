using System.Collections.Generic;
using UnityEngine;
public class Wizard : Range
{
    public Wizard(GameObject projectil) : base(projectil)
    {
        Projectil = projectil;
    }
}

