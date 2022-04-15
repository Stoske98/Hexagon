using System.Collections.Generic;
using UnityEngine;
public class Archer : Range
{
    public GameObject passiveImage;
    public bool passive;
    public override List<Hex> getAttackMoves(Hex hex)
    {
        List<Hex> hexes = base.getAttackMoves(hex);
        /* if((hex.Column == 8 && hex.Row == 2) || (hex.Column == 0 && hex.Row == 2) && (hex.Column == 8 && hex.Row == 6) || (hex.Column == 0 && hex.Row == 2))
         {
             addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column, hex.Row + 3));
             addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column, hex.Row - 3));
             addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column - 3, hex.Row - 2));
             addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column + 3, hex.Row - 2));
             addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column - 3, hex.Row + 1));
             addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column + 3, hex.Row + 1));
         }*/
        if(passive)
        {
            //top
            addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column, hex.Row + 3));
            //bottom
            addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column, hex.Row - 3));

            if (hex.Column % 2 != 0)
            {
                //topLeft
                addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column - 3, hex.Row - 1));
                //topRight
                addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column + 3, hex.Row - 1));
                //bottomLeft
                addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column - 3, hex.Row + 2));
                //bottomRight
                addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column + 3, hex.Row + 2));
            }
            else
            {
                //topLeft
                addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column - 3, hex.Row - 2));
                //topRight
                addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column + 3, hex.Row - 2));
                //bottomLeft
                addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column - 3, hex.Row + 1));
                //bottomRight
                addHexInAttackMoves(ref hexes, Map.Instance.GetHex(hex.Column + 3, hex.Row + 1));
            }
        }
        

        return hexes;

    }
    public Archer(GameObject projectil) : base(projectil)
    {
        Projectil = projectil;
    }

    public override List<Hex> getSpecialMoves(Hex hex)
    {
        return specialMoves;

    }

    private void addHexInAttackMoves(ref List<Hex> hexes, Hex hex)
    {
        Unit unit = Map.Instance.GetUnit(hex);
        if (hex != null && !hex.Walkable && unit != null && unit.Team != Team)
            hexes.Add(hex);
    }

    public virtual void ActivateArcherPassive()
    {
        if (passiveImage == null)
        {
            foreach (Transform child in GameObject.transform)
            {
                if (child.CompareTag("ArcherPassive"))
                    passiveImage = child.gameObject;
            }

        }
        passiveImage.SetActive(true);
        passiveImage.transform.LookAt(passiveImage.transform.position + GameManager.Instance.cameraAngles[GameManager.Instance.currentTeam + 1].transform.forward);
        passive = true;
    }

    public virtual void DeactivateArcherPassive()
    {
        if (passiveImage == null)
        {
            foreach (Transform child in GameObject.transform)
            {
                if (child.CompareTag("ArcherPassive"))
                    passiveImage = child.gameObject;
            }

        }
        passiveImage.SetActive(false);
        passive = false;
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


