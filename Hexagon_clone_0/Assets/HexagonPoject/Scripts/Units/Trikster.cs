using System.Collections.Generic;
using UnityEngine;
public class Trikster : Melee
{
    public TriksterIllusion ilusion1 = null;
    public TriksterIllusion ilusion2 = null;
    public override List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();

        specialMoves = PathFinder.BFS_ListInRange(hex, 2);

        return specialMoves;
    }
    public override void RecieveDamage(int damage)
    {
        if (ilusion1 != null && !ilusion1.isDeath)
            ilusion1.RecieveDamage(damage);
        if (ilusion2 != null && !ilusion2.isDeath)
            ilusion2.RecieveDamage(damage);
        base.RecieveDamage(damage);
    }
    public override void SetPath(Hex hex)
    {
        path = PathFinder.FindPath_AStar(Map.Instance.map[Column, Row], hex);
        if(path.Count > 2)
        {
            path.Clear();
            path.Add(hex);
        }    
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;
        animator.SetBool("Run", true);
    }
    public void createIllusion(ref TriksterIllusion triksterIllusion, Hex hex)
    {
        if (triksterIllusion != null)
        {
            if (triksterIllusion.Column != -1 && triksterIllusion.Row != -1)
                Map.Instance.GetHex(triksterIllusion.Column, triksterIllusion.Row).Walkable = true;
            triksterIllusion.Column = -1;
            triksterIllusion.Row = -1;
            triksterIllusion.GameObject.transform.position = new Vector3(1000, 1000, 1000);
        }

        GameObject illusionGO = GameManager.Instance.spawnUnit(UnitType.Trikster, Column, Row, GameManager.Instance.Classes[Team]);
        triksterIllusion = new TriksterIllusion
        {
            Column = Column,
            Row = Row,
            GameObject = illusionGO,

            Team = Team,

            MaxHealth = 3,
            CurrentHealth = CurrentHealth,

            Damage = 1,
            AttackRange = 0,
            attackSpeed = 0.25f
        };
        triksterIllusion.animator = triksterIllusion.GameObject.GetComponent<Animator>();
        GameManager.Instance.units.Add(triksterIllusion);

        triksterIllusion.healthBar = new HealthBar(GameManager.Instance.CanvasHBPrefab, GameManager.Instance.HealthImagePrefab);
        triksterIllusion.InitializeHealthBar();

        triksterIllusion.SetPath(hex);
    }
    public Vector2Int createIllusion(ref TriksterIllusion triksterIllusion)
    {

        if(triksterIllusion != null)
        {
            if(triksterIllusion.Column != -1 && triksterIllusion.Row != -1)
                Map.Instance.GetHex(triksterIllusion.Column, triksterIllusion.Row).Walkable = true;
            triksterIllusion.Column = -1;
            triksterIllusion.Row = -1;
            triksterIllusion.GameObject.transform.position = new Vector3(1000, 1000, 1000);
        }

        bool hasWalkableHex = false;
        foreach (var h in specialMoves)
        {
            if(h.Walkable)
            {
                hasWalkableHex = true;
                break;
            }
        }
        Hex hex = specialMoves[Random.Range(0, specialMoves.Count)]; 
        if (hasWalkableHex)
        {
            if(!hex.Walkable)
            {
                while (!hex.Walkable)
                        hex = specialMoves[Random.Range(0, specialMoves.Count)];
            }

            GameObject illusionGO = GameManager.Instance.spawnUnit(UnitType.Trikster, Column, Row, GameManager.Instance.Classes[Team]);
            triksterIllusion = new TriksterIllusion
            {
                Column = Column,
                Row = Row,
                GameObject = illusionGO,

                Team = Team,

                MaxHealth = 3,
                CurrentHealth = CurrentHealth,

                Damage = 1,
                AttackRange = 0,
                attackSpeed = 0.25f
            };
            triksterIllusion.animator = triksterIllusion.GameObject.GetComponent<Animator>();
            GameManager.Instance.units.Add(triksterIllusion);

            triksterIllusion.healthBar = new HealthBar(GameManager.Instance.CanvasHBPrefab, GameManager.Instance.HealthImagePrefab);
            triksterIllusion.InitializeHealthBar();

            triksterIllusion.SetPath(hex);
            return new Vector2Int(hex.Column,hex.Row);
        }
        triksterIllusion = null;
        return new Vector2Int(-1,-1);
    }
}

public class TriksterIllusion : Trikster
{
    

    public override List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();

        return specialMoves;
    }

    public override List<Hex> getAvailableMoves(Hex hex)
    {
        availableMoves.Clear();

        return availableMoves;
    }

    public override void RecieveDamage(int damage)
    {
        CurrentHealth = 0;
        isDeath = true;
        Map.Instance.GetHex(Column, Row).Walkable = true;
        Column = -1;
        Row = -1;
        GameObject.transform.position = new Vector3(1000, 1000, 1000);
    }
}

public class TriksterLight : Trikster
{ }
public class TriksterDark : Trikster
{

}



