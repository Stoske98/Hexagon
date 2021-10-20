using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tank : Melee
{
    public override List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();

        Hex firstHexInDirection = null;
        Hex secondHexInDirection = null;
        //top
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 2);
        AddHexesInDirection(firstHexInDirection, secondHexInDirection);
        //bottom
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 2);
        AddHexesInDirection(firstHexInDirection, secondHexInDirection);

        if (hex.Column % 2 != 0)
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection);
        }
        else
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection);
        }


        return availableMoves;
    }

    public override List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();

        Hex firstHexInDirection = null;
        Hex secondHexInDirection = null;
        //top
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 2);
        AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
        //bottom
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 2);
        AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);

        if (hex.Column % 2 != 0)
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
        }
        else
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            AddHexesInDirection(firstHexInDirection, secondHexInDirection, true);
        }


        return specialMoves;
    }

    public void AddHexesInDirection(Hex first, Hex second, bool specialMove = false)
    {
        if (first != null)
        {
            if (first.Walkable && !specialMove)
            {
                availableMoves.Add(first);
                if (second != null && second.Walkable)
                    availableMoves.Add(second);
            }
            else if (Map.Instance.GetUnit(first) != null && Map.Instance.GetUnit(first).Team != Team && specialMove)
            {
                AddHexToSpecialMoveList(second);
            }
        }
    }

    public override void SetPath(Hex hex)
    {
        path.Add(hex);
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;

    }
}

