using System.Collections.Generic;
using UnityEngine;

public class King : Melee
{
    /*private bool upgraded1;
    private bool upgraded2;
    private bool upgraded3;*/
    //private Unit unitToAttack;
    //List<SpecialMovesAttackFields> ksmaf;
    public override List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();
        /*ksmaf = new List<SpecialMovesAttackFields>();
        if (AttackRange == 1)
        {
            specialMoves = PathFinder.BFS_HexesInRange(hex, 2);


            foreach (Hex h in hex.neighbors)
            {
                if (specialMoves.Contains(h))
                    specialMoves.Remove(h);
            }

            List<Hex> enemyHexes = new List<Hex>();

            foreach (Hex eh in specialMoves)
            {
                Unit unit = Map.Instance.GetUnit(eh);
                if (unit != null && unit.Team != Team)
                    enemyHexes.Add(eh);
            }
            specialMoves.Clear();
            foreach (Hex eh in enemyHexes)
            {
                foreach (Hex neighbor in hex.neighbors)
                {
                    if (neighbor.Walkable && neighbor.neighbors.Contains(eh))
                    {
                        ksmaf.Add(new SpecialMovesAttackFields(neighbor, Map.Instance.GetUnit(eh), null));
                        specialMoves.Add(eh);
                    }
                }
            }
        }*/

        return specialMoves;
    }

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
                   /* foreach (SpecialMovesAttackFields sm in ksmaf)
                    {
                        if (sm.DesiredHex == path[0] && sm.FirstUnit == unitToAttack)
                        {
                            Attack(sm.FirstUnit);
                        }
                    }*/
                    isSpecialMove = false;
                }
            }
        }

    }
    public override void Move()
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
                    if(Column == 4 && Row == 4)
                    {
                        /*int mxh = MaxHealth;
                        if (!upgraded3 && ((Team == 0 && GameManager.Instance.deadP1Units.Count >= 8 && MaxHealth != 5) || (Team == 1 && GameManager.Instance.deadP2Units.Count >= 8 && MaxHealth != 5)))
                        {
                            MaxHealth = 5;
                            CurrentHealth += MaxHealth - mxh;
                            Damage = 3;
                            AttackRange = 1;
                            GameManager.Instance.DestroyGameObject(healthBar.CanvasHB);
                            healthBar = new HealthBar(GameManager.Instance.CanvasHBPrefab, GameManager.Instance.HealthImagePrefab);
                            InitializeHealthBar();
                            upgraded3 = true;
                            upgraded2 = true;
                            upgraded1 = true;

                        }
                        else if(!upgraded2 && ((Team == 0 && GameManager.Instance.deadP1Units.Count >= 4 && MaxHealth != 4) || (Team == 1 && GameManager.Instance.deadP2Units.Count >= 4 && MaxHealth != 4)))
                        {
                            MaxHealth = 4;
                            CurrentHealth += MaxHealth - mxh;
                            Damage = 2;
                            AttackRange = 1;
                            GameManager.Instance.DestroyGameObject(healthBar.CanvasHB);
                            healthBar = new HealthBar(GameManager.Instance.CanvasHBPrefab, GameManager.Instance.HealthImagePrefab);
                            InitializeHealthBar();
                            upgraded2 = true;
                            upgraded1 = true;
                        }
                        else if (!upgraded1 && ((Team == 0 && GameManager.Instance.deadP1Units.Count >= 3 && MaxHealth == 3) || (Team == 1 && GameManager.Instance.deadP2Units.Count >= 3 && MaxHealth == 3)))
                        {
                            Damage = 1;
                            AttackRange = 1;
                            GameManager.Instance.DestroyGameObject(healthBar.CanvasHB);
                            healthBar = new HealthBar(GameManager.Instance.CanvasHBPrefab, GameManager.Instance.HealthImagePrefab);
                            InitializeHealthBar();
                            upgraded1 = true;
                        }*/
                    }
                    animator.SetBool("Run", false);
                }
            }
        }
    }

    /*public override void SetPath(Hex hex)
    {
        if (isSpecialMove)
        {
            foreach (SpecialMovesAttackFields smaf in ksmaf)
            {
                if (smaf.FirstUnit.Column == hex.Column && smaf.FirstUnit.Row == hex.Row)
                {
                    unitToAttack = smaf.FirstUnit;
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
    }*/
}

public class KingLight : King
{ }
public class KingDark : King
{

}

