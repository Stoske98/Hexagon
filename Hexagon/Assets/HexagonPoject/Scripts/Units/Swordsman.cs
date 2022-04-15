using System.Collections.Generic;
using UnityEngine;
public class Swordsman : Melee
{
    public List<Unit> frontUnits;

    public Swordsman() : base()
    {
        frontUnits = new List<Unit>();
    }

    public override void Attack()
    {
        if (animator.GetBool("Attack") && Time.time >= currentTime + attackSpeed)
        {
            int enemyColumn = EnemyUnit.Column;
            int enemyRow = EnemyUnit.Row;
            EnemyUnit.RecieveDamage(Damage);
            if (EnemyUnit.isDeath && enemyColumn != -1 && enemyRow != -1)
                SetPath(Map.Instance.GetHex(enemyColumn, enemyRow));
            EnemyUnit = null;
            animator.SetBool("Attack", false);
            UpdatePassive();
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
                    if(animator.GetBool("Run"))
                    {
                        UpdatePassive();
                        animator.SetBool("Run", false);
                    }
                }
            }
        }
    }
    public void PassiveAttack()
    {
        List<Unit> unitsToRemove = new List<Unit>();
        if(frontUnits.Count != 0)
        {
            foreach (Unit unit in frontUnits)
            {
                if((unit.GameObject.transform.position - GameObject.transform.position).magnitude > 1.2f)
                {
                    unitsToRemove.Add(unit);
                }
            }
        }

        if(unitsToRemove.Count != 0)
        {
            foreach (Unit unit in unitsToRemove)
            {
                frontUnits.Remove(unit);
            }
        }
        bool attack = false;
        foreach (Unit unit in GameManager.Instance.units)
        {
            if(unit.Team != Team && !frontUnits.Contains(unit) && unit.CurrentHealth != 0 && !unit.isDeath)
            {

                if ((unit.GameObject.transform.position - GameObject.transform.position).magnitude < 1.2f)
                {                    
                    if (Vector3.Dot(GameObject.transform.forward, unit.GameObject.transform.position - GameObject.transform.position) > 0)
                    {
                        if(!attack)
                        {
                            Attack(unit);
                            attack = true;
                        }
                        frontUnits.Add(unit);
                    }
                }
            }
        }
    }

    public void UpdatePassive()
    {
        List<Unit> unitsToRemove = new List<Unit>();
        if (frontUnits.Count != 0)
        {
            foreach (Unit unit in frontUnits)
            {
                if ((unit.GameObject.transform.position - GameObject.transform.position).magnitude > 1.2f)
                {
                    unitsToRemove.Add(unit);
                }
            }
        }

        if (unitsToRemove.Count != 0)
        {
            foreach (Unit unit in unitsToRemove)
            {
                frontUnits.Remove(unit);
            }
        }
        foreach (Unit unit in GameManager.Instance.units)
        {
            if (unit.Team != Team && !frontUnits.Contains(unit) && unit.CurrentHealth != 0 && !unit.isDeath)
            {

                if ((unit.GameObject.transform.position - GameObject.transform.position).magnitude < 1.2f)
                {
                    if (Vector3.Dot(GameObject.transform.forward, unit.GameObject.transform.position - GameObject.transform.position) > 0)
                    {
                        frontUnits.Add(unit);
                    }
                }
            }
        }

    }


}
public class SwordsmanLight : Swordsman
{
}
public class SwordsmanDark : Swordsman
{
}

