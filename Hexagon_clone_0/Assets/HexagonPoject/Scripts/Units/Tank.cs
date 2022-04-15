using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tank : Melee
{
    Stun stun = null;
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
                if (path.Count == 1)
                {
                    stun.CreateCC(2);
                    cc.Add(stun);
                    stun.Unit.RecieveDamage(1);
                }
                if (path.Count - 1 != 0)
                    path.RemoveAt(0);
                else
                    isSpecialMove = false;
            }
        }

    }

    public override List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();

        Hex firstHexInDirection = null;
        Hex secondHexInDirection = null;
        Hex thirdHexInDirection = null;
        //top
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 2);
        AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
        //bottom
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 2);
        AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);

        if (hex.Column % 2 != 0)
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
        }
        else
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            AddHexesToAvailableList(firstHexInDirection, secondHexInDirection);
        }


        return availableMoves;
    }
    public void AddHexesToAvailableList(Hex first, Hex second)
    {
        if (AddHexToAvailableList(first))
            AddHexToAvailableList(second);
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

        if (isSpecialMove)
        {
            path = PathFinder.FindPath_AStar_WithNoneWalkableFields(Map.Instance.map[Column, Row], hex);
            stun = new Stun
            {
                Unit = Map.Instance.GetUnit(path[path.Count - 2])
            };
        }
        else
            path.Add(hex);
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;
        animator.SetBool("Run", true);

    }
}

public class TankLight : Tank
{ }
public class TankDark : Tank
{ }


