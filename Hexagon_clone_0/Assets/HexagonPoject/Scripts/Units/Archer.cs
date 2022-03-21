using System.Collections.Generic;
using UnityEngine;
public class Archer : Range
{
    public override List<Hex> getAttackMoves(Hex hex)
    {
        List<Hex> hexes = base.getAttackMoves(hex);
        if((hex.Column == 8 && hex.Row == 2) || (hex.Column == 0 && hex.Row == 2) && (hex.Column == 8 && hex.Row == 6) || (hex.Column == 0 && hex.Row == 2))
        {
            addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column, hex.Row + 3));
            addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column, hex.Row - 3));
            addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column - 3, hex.Row - 2));
            addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column + 3, hex.Row - 2));
            addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column - 3, hex.Row + 1));
            addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column + 3, hex.Row + 1));
        }
        
        return hexes;

    }
    public Archer(GameObject projectil) : base(projectil)
    {
        Projectil = projectil;
    }

    public override List<Hex> getSpecialMoves(Hex hex)
    {
        /*if (hex.neighbors.Contains(Map.Instance.GetHex(0, 2)))
            specialMoves.Add(Map.Instance.GetHex(0, 2));
        if (hex.neighbors.Contains(Map.Instance.GetHex(8, 2)))
            specialMoves.Add(Map.Instance.GetHex(8, 2));
        if (hex.neighbors.Contains(Map.Instance.GetHex(8, 6)))
            specialMoves.Add(Map.Instance.GetHex(8, 6));
        if (hex.neighbors.Contains(Map.Instance.GetHex(0, 6)))
            specialMoves.Add(Map.Instance.GetHex(0, 6));*/
        return specialMoves;

    }

    private void addHexInAttackMoves(ref List<Hex> hexes, Hex hex)
    {
        Unit unit = Map.Instance.GetUnit(hex);
        if (hex != null && !hex.Walkable && unit != null && unit.Team != Team)
            hexes.Add(hex);
    }

}
public class ArcherLight : Archer
{
    public ArcherLight(GameObject projectil) : base(projectil)
    {
        Projectil = projectil;
    }
}

public class ArcherDark : Archer
{
    public ArcherDark(GameObject projectil) : base(projectil)
    {
        Projectil = projectil;
    }
}


