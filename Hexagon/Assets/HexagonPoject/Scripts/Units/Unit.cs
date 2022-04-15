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

public enum ClassType
{
    Light = 0,
    Dark = 1
}


public enum UnitState
{
    None = 0,
    Stuned = 1,
}

public class Unit
{

    public const float MOVEMENTSPEED = 2F;
    public const float ROTATESPEED = 5F;

    public int Column;
    public int Row;
    public int Team;

    public int MaxHealth;
    public int CurrentHealth;
    public int Damage;
    public int AttackRange;
    public Sprite sprite;

    public GameObject GameObject;
    public UnitState currentState = UnitState.None;
    public UnitType unitType;
    public List<Hex> path;
    public List<Hex> availableMoves;
    public List<Hex> specialMoves;
    public List<CC> cc;
    public HealthBar healthBar;
    public Quaternion targetRotation;
    public bool isSpecialMove = false;
    public Unit EnemyUnit;
    public Animator animator;
    public float attackSpeed = 0.0f;
    public float currentTime = 0.0f;
    public bool isDeath = false;
    public Ability ability1;
    public Ability ability2;
    public Unit()
    {
        cc = new List<CC>();
        availableMoves = new List<Hex>();
        specialMoves = new List<Hex>();
        path = new List<Hex>();
    }

    public void InitializeHealthBar()
    {
        healthBar.Initialize(this);
    }

    public void UpdateBar(GameObject camera)
    {
        healthBar.HealthBarFiller(this);
        healthBar.ColorChanger(this);
        healthBar.CanvasHB.transform.LookAt(healthBar.CanvasHB.transform.position + camera.transform.forward);
    }
    public virtual void Update()
    {
        if (!isDeath)
        {
            if (isSpecialMove)
                SpecialMove();
            else
                Move();
            Attack();
        }
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
                else
                {

                    if (animator.GetBool("Run"))
                    {
                        animator.SetBool("Run", false);
                    }
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
                    Debug.Log("end special move");
                    isSpecialMove = false;
                }
            }
        }

    }

    public virtual void Attack(Unit enemyUnit)
    {
        EnemyUnit = enemyUnit;
    }

    public virtual void Attack()
    {
        EnemyUnit.RecieveDamage(Damage);
        EnemyUnit = null;
    }
    public virtual List<Hex> getAttackMoves(Hex hex)
    {
        List<Hex> attackMoves = new List<Hex>();
        foreach (Hex h in PathFinder.BFS_HexesInRange(hex,AttackRange))
        {
            Unit unit = Map.Instance.GetUnit(h);
            if (unit != null && unit.Team != Team)
                attackMoves.Add(h);
        }
        return attackMoves;
    }
    public virtual List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();
        foreach (Hex h in hex.neighbors)
        {
            if (h.Walkable)
            {
                availableMoves.Add(h);
            }
        }

        return availableMoves;
    }

    public bool AddHexToAvailableList(Hex hex)
    {
        if (hex != null && hex.Walkable)
        {
            availableMoves.Add(hex);
            return true;
        }
        return false;
        

    }
    public bool AddHexToSpecialMoveList(Hex hex)
    {
        if(hex != null && hex.Walkable)
        {
            specialMoves.Add(hex);
            return true;
        }
        return false;
    }

    public virtual List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();
        return specialMoves;
    }

    
    public virtual void SetPath(Hex hex)
    {
        path = PathFinder.FindPath_AStar(Map.Instance.map[Column, Row], hex);
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;
        animator.SetBool("Run", true);
    }

    public virtual void RecieveDamage(int damage)
    {
        if (CurrentHealth - damage > 0)
            CurrentHealth -= damage;
        else
        {
            if(Column != -1 && Row != -1)
            {
                isDeath = true;
                animator.SetBool("Death", true);
                GameManager.Instance.OnUnitDeath(3f,this);
                GameUiManager.Instance.UpdateTopBarStatus(this);
            }
        }
           
    }

   
}

/*
 public List<Hex> FrontHexes(List<Hex> hexes_path)
    {
        Hex desiredHex = hexes_path[hexes_path.Count - 1];
        Hex hexBeforeDesiredHex = hexes_path[hexes_path.Count - 2];

        List<Hex> fronthexes = new List<Hex>();

        foreach (Hex neighbor in desiredHex.neighbors)
        {
            if (!hexBeforeDesiredHex.neighbors.Contains(neighbor) && neighbor != hexBeforeDesiredHex)
                fronthexes.Add(neighbor);
        }

        return fronthexes;
    }

    public List<Hex> BackHexes(List<Hex> hexes_path)
    {
        Hex desiredHex = hexes_path[hexes_path.Count - 1];
        Hex hexBeforeDesiredHex = hexes_path[hexes_path.Count - 2];

        List<Hex> fronthexes = new List<Hex>();

        foreach (Hex neighbor in desiredHex.neighbors)
        {
            if (hexBeforeDesiredHex.neighbors.Contains(neighbor) || neighbor == hexBeforeDesiredHex)
                fronthexes.Add(neighbor);
        }

        return fronthexes;
    }
 
 */

