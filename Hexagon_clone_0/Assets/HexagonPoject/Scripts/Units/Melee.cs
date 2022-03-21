using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Unit
{

    public override void Attack()
    {
        if (animator.GetBool("Attack") && Time.time >= currentTime + attackSpeed)
        {
            int enemyColumn = EnemyUnit.Column;
            int enemyRow = EnemyUnit.Row;
            EnemyUnit.RecieveDamage(Damage);
            if (EnemyUnit.isDeath)
                SetPath(Map.Instance.GetHex(enemyColumn, enemyRow));
            EnemyUnit = null;
            animator.SetBool("Attack", false);
        }
    }

    public override void Attack(Unit enemyUnit)
    {
        if (EnemyUnit == null)
        {
            GameObject.transform.rotation = Quaternion.LookRotation(enemyUnit.GameObject.transform.position - GameObject.transform.position, Vector3.up);
            currentTime = Time.time;
            EnemyUnit = enemyUnit;
            animator.SetBool("Attack", true);
        }
    }

}
