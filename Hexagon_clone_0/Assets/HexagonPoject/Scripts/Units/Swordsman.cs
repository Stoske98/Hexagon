using System.Collections.Generic;
using UnityEngine;
public class Swordsman : Melee
{
    public List<Unit> frontUnits;

    public Swordsman() : base()
    {
        frontUnits = new List<Unit>();
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
                        }
                        Debug.Log("HIT: " + (unit.GameObject.transform.position - GameObject.transform.position).magnitude);
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

