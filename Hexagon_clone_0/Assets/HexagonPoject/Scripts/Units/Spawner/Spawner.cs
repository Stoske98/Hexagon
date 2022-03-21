using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    public virtual Unit spawnUnit(int col, int row, GameObject unitPrefabm, int team, ClassType type) { return null; }

}

public class SpawnSwordsman : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team, ClassType type)
    {
        switch (type)
        {
            case ClassType.Light:
                SwordsmanLight sl = new SwordsmanLight
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,
                    Team = team,

                    MaxHealth = 2,
                    CurrentHealth = 2,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                sl.animator = sl.GameObject.GetComponent<Animator>();
                return sl;
            case ClassType.Dark:
                SwordsmanDark sd = new SwordsmanDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,
                    Team = team,

                    MaxHealth = 2,
                    CurrentHealth = 2,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                sd.animator =sd.GameObject.GetComponent<Animator>();
                return sd;
            default:
                return null;
        }
      
    }
}

public class SpawnTank : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team, ClassType type)
    {
        switch (type)
        {
            case ClassType.Light:
                TankLight tl = new TankLight
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 4,
                    CurrentHealth = 4,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                tl.animator = tl.GameObject.GetComponent<Animator>();
                return tl;
            case ClassType.Dark:
                TankDark td = new TankDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 4,
                    CurrentHealth = 4,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                td.animator = td.GameObject.GetComponent<Animator>();
                return td;
            default:
                return null;
        }

    }
}

public class SpawnQueen : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team, ClassType type)
    {
        switch (type)
        {
            case ClassType.Light:
                QueenLight ql = new QueenLight
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 2,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                ql.animator = ql.GameObject.GetComponent<Animator>();
                return ql;
            case ClassType.Dark:
                QueenDark qd = new QueenDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 2,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                qd.animator = qd.GameObject.GetComponent<Animator>();
                return qd;
            default:
                return null;
        }

    }
}
public class SpawnKing : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team, ClassType type)
    {
        switch (type)
        {
            case ClassType.Light:
                KingLight kl = new KingLight
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                kl.animator = kl.GameObject.GetComponent<Animator>();
                return kl;
            case ClassType.Dark:
                KingDark kd = new KingDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                kd.animator = kd.GameObject.GetComponent<Animator>();
                return kd;
            default:
                return null;
        }

    }
}

public class SpawnArcher : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team, ClassType type)
    {
        switch (type)
        {
            case ClassType.Light:
                ArcherLight al = new ArcherLight(GameManager.Instance.projectilPrefab)
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 2,
                    CurrentHealth = 2,

                    Damage = 1,
                    AttackRange = 2,
                    attackSpeed = 0.25f
                };
                al.animator = al.GameObject.GetComponent<Animator>();
                return al;
            case ClassType.Dark:
                ArcherDark ad = new ArcherDark(GameManager.Instance.projectilPrefab)
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 2,
                    CurrentHealth = 2,

                    Damage = 1,
                    AttackRange = 2,
                    attackSpeed = 0.25f
                };
                ad.animator = ad.GameObject.GetComponent<Animator>();
                return ad;
            default:
                return null;
        }

    }
}

public class SpawnWizard : Spawner
{ 
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team, ClassType type)
    {
        switch (type)
        {
            case ClassType.Light:
                WizardLight wl = new WizardLight
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 0,
                    attackSpeed = 0.25f
                };
                wl.animator = wl.GameObject.GetComponent<Animator>();
                return wl;
            case ClassType.Dark:
                WizardDark wd = new WizardDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 0,
                    attackSpeed = 0.25f
                };
                wd.animator = wd.GameObject.GetComponent<Animator>();
                return wd;
            default:
                return null;
        }

    }


}

public class SpawnTrikset : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team, ClassType type)
    {
        switch (type)
        {
            case ClassType.Light:
                TriksterLight tl = new TriksterLight
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                tl.animator = tl.GameObject.GetComponent<Animator>();
                return tl;
            case ClassType.Dark:
                TriksterDark td = new TriksterDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                td.animator = td.GameObject.GetComponent<Animator>();
                return td;
            default:
                return null;
        }

    }
}



public class SpawnKnight : Spawner
{

    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team, ClassType type)
    {
        switch (type)
        {
            case ClassType.Light:
                KnightLight kl = new KnightLight
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                kl.animator = kl.GameObject.GetComponent<Animator>();
                return kl;
            case ClassType.Dark:
                KnightDark kd = new KnightDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                kd.animator = kd.GameObject.GetComponent<Animator>();
                return kd;
            default:
                return null;
        }

    }
}
