using System.Collections.Generic;
using UnityEngine;
public class Archer : Range
{
    public Archer(GameObject projectil) : base(projectil)
    {
        Projectil = projectil;
    }

    public override List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();

        availableMoves = PathFinder.BFS_ListInRange(hex, 2);

        return availableMoves;
    }

}

