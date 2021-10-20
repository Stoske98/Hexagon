using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    None = 0,
    Swordsman = 1,
    Tank = 2,
    Knight = 3,
    Archer = 4,
    Trikster = 5,
    Wizzard = 6,
    Queen = 7,
    King = 8
}

public class Unit
{

    public const float MOVEMENTSPEED = 5F;
    public const float ROTATESPEED = 5F;

    public int Column;
    public int Row;
    public int Team;

    public int MaxHealth;
    public int CurrentHealth;
    public int Damage;
    public int AttackRange;

    public GameObject GameObject;
    public List<Hex> path;
    public List<Hex> availableMoves;
    public List<Hex> specialMoves;
    public Quaternion targetRotation;
    public bool isSpecialMove = false;
    public Unit EnemyUnit;
    public Unit()
    {
        availableMoves = new List<Hex>();
        specialMoves = new List<Hex>();
        path = new List<Hex>();
        CurrentHealth = MaxHealth;
    }

    public virtual void Update()
    {
        if (isSpecialMove)
            SpecialMove();
        else
            Move();
    }

    public virtual void Move()
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
            }
        }
    }

    public virtual void SpecialMove()
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
                    isSpecialMove = false;
                }
            }
        }

    }

    public virtual void Attack(Unit enemyUnit)
    {
        Debug.Log("Attack");
        EnemyUnit = enemyUnit;
    }

    public virtual void Attack()
    {

    }

    public virtual List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();
        foreach (Hex h in hex.neighbors)
        {
            if (h.Walkable)
                availableMoves.Add(h);
        }

        return availableMoves;
    }

    public void AddHexToAvailableList(Hex hex)
    {
        if (hex != null && hex.Walkable)
            availableMoves.Add(hex);

    }
    public void AddHexToSpecialMoveList(Hex hex)
    {
        if (hex != null && hex.Walkable)
            specialMoves.Add(hex);
    }

    public virtual List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();
        return specialMoves;
    }

    public virtual void isItASpecialMove(List<Hex> hexes, Hex hex)
    {
        if (hexes.Contains(hex))
            isSpecialMove = true;
        else
            isSpecialMove = false;

    }

    public virtual void SetPath(Hex hex)
    {
        path = PathFinder.FindPath_AStar(Map.Instance.map[Column, Row], hex);
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;
    }
}

