using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Queen : Melee
{
    public override List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();

        Hex firstHexInDirection = null;
        Hex secondHexInDirection = null;
        Hex thirdHexInDirection = null;
        //top
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 2);
        thirdHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 3);
        AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
        //bottom
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 2);
        thirdHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 3);
        AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);

        if (hex.Column % 2 != 0)
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column - 3, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column + 3, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column - 3, hex.Row + 2);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column + 3, hex.Row + 2);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
        }
        else
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column - 3, hex.Row - 2);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column + 3, hex.Row - 2);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column - 3, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column + 3, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
        }


        return availableMoves;
    }

    public void AddHexesInDirection(Hex first, Hex second, Hex third, bool specialMove = false)
    {
        if (first != null)
        {
            if (first.Walkable && !specialMove)
            {
                availableMoves.Add(first);
                if (second != null && second.Walkable)
                {
                    availableMoves.Add(second);
                    if (third != null && third.Walkable)
                        availableMoves.Add(third);
                }
            }
            /*else if (Map.Instance.GetUnitFromHex(first) != null && Map.Instance.GetUnitFromHex(first).Team != Team && specialMove)
            {
                if (second != null && second.Walkable)
                    hexes.Add(second);
            }*/
        }
    }
}

