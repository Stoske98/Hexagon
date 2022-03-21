using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Queen : Melee
{
    List<SpecialMovesAttackFields> qsmaf;
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
                }
                else
                {
                    Debug.Log("end special move");
                    foreach (SpecialMovesAttackFields specialMoves in qsmaf)
                    {
                        if (specialMoves.DesiredHex == path[0])
                        {
                            Attack(specialMoves.FirstUnit);
                        }
                    }
                    isSpecialMove = false;
                }
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

    public override List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();
        qsmaf = new List<SpecialMovesAttackFields>();
        Hex firstHexInDirection = null;
        Hex secondHexInDirection = null;
        Hex thirdHexInDirection = null;
        //top
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 2);
        thirdHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row + 3);
        AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
        //bottom
        firstHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 1);
        secondHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 2);
        thirdHexInDirection = Map.Instance.GetHex(hex.Column, hex.Row - 3);
        AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);

        if (hex.Column % 2 != 0)
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column - 3, hex.Row - 1);
            AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column + 3, hex.Row - 1);
            AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column - 3, hex.Row + 2);
            AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row + 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column + 3, hex.Row + 2);
            AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
        }
        else
        {
            //topLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row - 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column - 3, hex.Row - 2);
            AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //topRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row - 1);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row - 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column + 3, hex.Row - 2);
            AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //bottomLeft
            firstHexInDirection = Map.Instance.GetHex(hex.Column - 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column - 2, hex.Row + 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column - 3, hex.Row + 1);
            AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
            //bottomRight
            firstHexInDirection = Map.Instance.GetHex(hex.Column + 1, hex.Row);
            secondHexInDirection = Map.Instance.GetHex(hex.Column + 2, hex.Row + 1);
            thirdHexInDirection = Map.Instance.GetHex(hex.Column + 3, hex.Row + 1);
            AddHexesInSpecialMove(firstHexInDirection, secondHexInDirection, thirdHexInDirection);
        }


        return specialMoves;
    }

    public void AddHexesInDirection(Hex first, Hex second, Hex third)
    {
        if (AddHexToAvailableList(first))
            if (AddHexToAvailableList(second))
                AddHexToAvailableList(third);
    }

    public void AddHexesInSpecialMove(Hex first, Hex second, Hex third)
    {
        if (first != null && first.Walkable)
        {
            if(second != null)
            {
                if(!second.Walkable)
                {
                    if (Map.Instance.GetUnit(second) != null && Map.Instance.GetUnit(second).Team != Team)
                    {
                        qsmaf.Add(new SpecialMovesAttackFields(first, Map.Instance.GetUnit(second), null));
                        specialMoves.Add(second);
                    }
                }else
                {
                    if(third != null && !third.Walkable)
                    {
                        if (Map.Instance.GetUnit(third) != null && Map.Instance.GetUnit(third).Team != Team)
                        {
                            qsmaf.Add(new SpecialMovesAttackFields(second, Map.Instance.GetUnit(third), null));
                            specialMoves.Add(third);
                        }
                            
                    }
                }
            }
        }
    }

    public override void SetPath(Hex hex)
    {
        if (isSpecialMove)
        {
            foreach (SpecialMovesAttackFields smaf in qsmaf)
            {
                if (smaf.FirstUnit.Column == hex.Column && smaf.FirstUnit.Row == hex.Row)
                {
                    hex = smaf.DesiredHex;
                    break;
                }
            }
        }
        path = PathFinder.FindPath_AStar(Map.Instance.map[Column, Row], hex);
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;
        animator.SetBool("Run", true);
    }
}

public class QueenLight : Queen
{ }
public class QueenDark : Queen
{ }

