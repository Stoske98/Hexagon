using System.Collections.Generic;
using UnityEngine;
public class Knight : Melee
{
    List<SpecialMovesAttackFields> ksmaf;

    public override void SpecialMove()
    {
        if (path.Count != 0)
        {
            if ((path[0].GameObject.transform.position - GameObject.transform.position).magnitude > 0.1f)
            {
                GameObject.transform.position += (path[0].GameObject.transform.position - GameObject.transform.position).normalized * MOVEMENTSPEED * Time.deltaTime;

                targetRotation = Quaternion.LookRotation(path[0].GameObject.transform.position - GameObject.transform.position, Vector3.up);
                GameObject.transform.rotation = Quaternion.Slerp(GameObject.transform.rotation, targetRotation, Time.deltaTime * ROTATESPEED);
            }
            else
            {
                if (path.Count - 1 != 0)
                {
                    path.RemoveAt(0);
                    if(path.Count == 1)
                    {
                        foreach (SpecialMovesAttackFields specialMoves in ksmaf)
                        {
                            if (specialMoves.DesiredHex == path[0])
                            {
                                if (specialMoves.FirstUnit != null && specialMoves.FirstUnit.Team != Team)
                                    specialMoves.FirstUnit.RecieveDamage(1);
                                if (specialMoves.SecondUnit != null && specialMoves.SecondUnit.Team != Team)
                                    specialMoves.SecondUnit.RecieveDamage(1);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("end special move");
                    isSpecialMove = false;
                }
            }
        }

    }
    public override List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();

        //right
        AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 2, hex.Row));
        //left
        AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 2, hex.Row));

        if (hex.Column % 2 == 0)
        {
            //top right
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 1, hex.Row + 1));
            //top left
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 1, hex.Row + 1));
            //botom left
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 1, hex.Row - 2));
            //bottom right
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 1, hex.Row - 2));
        }
        else
        {
            //top left
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 1, hex.Row + 2));
            //top right
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 1, hex.Row + 2));
            //botom left
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column - 1, hex.Row - 1));
            //bottom right
            AddHexToAvailableList(Map.Instance.GetHex(hex.Column + 1, hex.Row - 1));
        }
        return availableMoves;
    }
    public override List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();
        ksmaf = new List<SpecialMovesAttackFields>();

        Hex desiredHex = null;
        Hex betweenHex1 = null;
        Hex betweenHex2 = null;



        if (hex.Column % 2 == 0)
        {
            //right
            desiredHex = Map.Instance.GetHex(hex.Column + 2, hex.Row);
            betweenHex1 = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //left
            desiredHex = Map.Instance.GetHex(hex.Column - 2, hex.Row);
            betweenHex1 = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //top right
            desiredHex = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row + 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //top left
            desiredHex = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row + 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //bottom left
            desiredHex = Map.Instance.GetHex(hex.Column - 1, hex.Row - 2);
            betweenHex1 = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column, hex.Row - 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //bottom right
            desiredHex = Map.Instance.GetHex(hex.Column + 1, hex.Row - 2);
            betweenHex1 = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column, hex.Row - 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
        }
        else
        {
            //right
            desiredHex = Map.Instance.GetHex(hex.Column + 2, hex.Row);
            betweenHex1 = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //left
            desiredHex = Map.Instance.GetHex(hex.Column - 2, hex.Row);
            betweenHex1 = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //top right
            desiredHex = Map.Instance.GetHex(hex.Column + 1, hex.Row + 2);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row + 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //top left
            desiredHex = Map.Instance.GetHex(hex.Column - 1, hex.Row + 2);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row + 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //bottom right
            desiredHex = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row - 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
            //bottom left
            desiredHex = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            betweenHex1 = Map.Instance.GetHex(hex.Column, hex.Row - 1);
            betweenHex2 = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            addToSpecialMove(desiredHex, betweenHex1, betweenHex2);
        }

        //top
        AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column, hex.Row + 1)), Map.Instance.GetHex(hex.Column, hex.Row + 2));
        //bottom
        AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column, hex.Row - 1)), Map.Instance.GetHex(hex.Column, hex.Row - 2));
        if (hex.Column % 2 != 0)
        {
            //topLeft
            AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column - 1, hex.Row)), Map.Instance.GetHex(hex.Column - 2, hex.Row - 1));
            //topRight
            AddHexesInDirection(ref specialMoves,Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column + 1, hex.Row)), Map.Instance.GetHex(hex.Column + 2, hex.Row - 1));
            //bottomLeft
            AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column - 1, hex.Row + 1)), Map.Instance.GetHex(hex.Column - 2, hex.Row + 1));
            //bottomRight
            AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column + 1, hex.Row + 1)), Map.Instance.GetHex(hex.Column + 2, hex.Row + 1));

        }
        else
        {
            //topLeft
            AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column - 1, hex.Row - 1)), Map.Instance.GetHex(hex.Column - 2, hex.Row - 1));
            //topRight
            AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column + 1, hex.Row - 1)), Map.Instance.GetHex(hex.Column + 2, hex.Row - 1));
            //bottomLeft
            AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column - 1, hex.Row)), Map.Instance.GetHex(hex.Column - 2, hex.Row + 1));
            //bottomRight
            AddHexesInDirection(ref specialMoves, Map.Instance.GetUnit(Map.Instance.GetHex(hex.Column + 1, hex.Row)), Map.Instance.GetHex(hex.Column + 2, hex.Row + 1));
        }

        return specialMoves;
    }

    private void AddHexesInDirection(ref List<Hex> hexes, Unit enemyUnit, Hex hex)
    {
        if (enemyUnit != null && enemyUnit.Team != this.Team && hex != null && hex.Walkable)
        {
            hexes.Add(Map.Instance.GetHex(enemyUnit.Column, enemyUnit.Row));
        }
    }

    private void addToSpecialMove(Hex desiredHex, Hex betweenHex1, Hex betweenHex2)
    {
        if (Map.Instance.GetUnit(betweenHex1) != null && Map.Instance.GetUnit(betweenHex1).Team != Team || Map.Instance.GetUnit(betweenHex2) != null && Map.Instance.GetUnit(betweenHex2).Team != Team)
        {
            ksmaf.Add(new SpecialMovesAttackFields( desiredHex, Map.Instance.GetUnit(betweenHex1), Map.Instance.GetUnit(betweenHex2)));
            AddHexToSpecialMoveList(desiredHex);
        }
    }
    public override void SetPath(Hex hex)
    {
        if(isSpecialMove && PathFinder.InRange(Map.Instance.map[Column, Row], hex, 1))
        {
            Unit unit = Map.Instance.GetUnit(hex);
            unit.SetPath(AvailableHex(hex));
            path = PathFinder.FindPath_AStar(Map.Instance.map[Column, Row], hex);
            Map.Instance.map[Column, Row].Walkable = true;
            Column = hex.Column;
            Row = hex.Row;
            hex.Walkable = false;
            animator.Play("Special");
            return;
        }
        path.Add(hex);
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;
        animator.SetBool("Run", true);
    }

    private Hex AvailableHex(Hex hex)
    {
        if (hex.Column == Column && hex.Row == Row + 1)
            return Map.Instance.GetHex(Column, Row + 2);
        if (hex.Column == Column && hex.Row == Row - 1)
            return Map.Instance.GetHex(Column, Row - 2);
        if (Column % 2 != 0)
        {
            if (hex.Column == Column - 1 && hex.Row == Row)
                return Map.Instance.GetHex(Column - 2, Row - 1);
            if (hex.Column == Column + 1 && hex.Row == Row)
                return Map.Instance.GetHex(Column + 2, Row - 1);
            if (hex.Column == Column - 1 && hex.Row == Row + 1)
                return Map.Instance.GetHex(Column - 2, Row + 1);
            if (hex.Column == Column + 1 && hex.Row == Row + 1)
                return Map.Instance.GetHex(Column + 2, Row + 1);
        }
        else
        {
            if (hex.Column == Column - 1 && hex.Row == Row - 1)
                return Map.Instance.GetHex(Column - 2, Row - 1);
            if (hex.Column == Column + 1 && hex.Row == Row - 1)
                return Map.Instance.GetHex(Column + 2, Row - 1);
            if (hex.Column == Column - 1 && hex.Row == Row)
                return Map.Instance.GetHex(Column - 2, Row + 1);
            if (hex.Column == Column + 1 && hex.Row == Row)
                return Map.Instance.GetHex(Column + 2, Row + 1);
        }
        return null;
    }
}

public struct SpecialMovesAttackFields
{
    public SpecialMovesAttackFields(Hex desiredHex, Unit firstUnit, Unit secondUnit)
    {
        DesiredHex = desiredHex;
        FirstUnit = firstUnit;
        SecondUnit = secondUnit;
    }
    public Hex DesiredHex { get; set; }
    public Unit FirstUnit { get; set; }
    public Unit SecondUnit { get; set; }

}
public class KnightLight : Knight
{
}
public class KnightDark : Knight
{
}


