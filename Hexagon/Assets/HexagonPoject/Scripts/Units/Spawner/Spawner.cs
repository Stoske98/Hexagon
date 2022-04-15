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

                    MaxHealth = 1,
                    CurrentHealth = 1,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                sl.unitType = UnitType.Swordsman;
                sl.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Swordsman");
                sl.animator = sl.GameObject.GetComponent<Animator>();
                return sl;
            case ClassType.Dark:
                SwordsmanDark sd = new SwordsmanDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,
                    Team = team,

                    MaxHealth = 1,
                    CurrentHealth = 1,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                sd.unitType = UnitType.Swordsman;
                sd.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Swordsman");
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

                    MaxHealth = 3,
                    CurrentHealth = 3,

                    Damage = 1,
                    AttackRange = 1,
                    attackSpeed = 0.25f
                };
                tl.unitType = UnitType.Tank;
                tl.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Tank");
                tl.animator = tl.GameObject.GetComponent<Animator>();
                return tl;
            case ClassType.Dark:
                TankDark td = new TankDark
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
                td.unitType = UnitType.Tank;
                td.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Tank");
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
                ql.unitType = UnitType.Queen;
                ql.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Queen");
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
                qd.unitType = UnitType.Queen;
                qd.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Queen");
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

                    MaxHealth = 4,
                    CurrentHealth = 4,

                    Damage = 0,
                    AttackRange = 0,
                    attackSpeed = 0.25f
                };
                kl.unitType = UnitType.King;
                kl.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/King");
                kl.animator = kl.GameObject.GetComponent<Animator>();
                return kl;
            case ClassType.Dark:
                KingDark kd = new KingDark
                {
                    Column = col,
                    Row = row,
                    GameObject = unitPrefab,

                    Team = team,

                    MaxHealth = 4,
                    CurrentHealth = 4,

                    Damage = 0,
                    AttackRange = 0,
                    attackSpeed = 0.25f
                };
                kd.unitType = UnitType.King;
                kd.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/King");
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
                al.unitType = UnitType.Archer;
                al.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Archer");
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
                ad.unitType = UnitType.Archer;
                ad.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Archer");
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
                wl.unitType = UnitType.Wizzard;
                wl.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Wizard");
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
                wd.unitType = UnitType.Wizzard;
                wd.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Wizard");
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
                tl.unitType = UnitType.Trikster;
                tl.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Jester");
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
                td.unitType = UnitType.Trikster;
                td.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Jester");
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
                kl.unitType = UnitType.Knight;
                kl.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Knight");
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
                kd.unitType = UnitType.Knight;
                kd.sprite = GameManager.Instance.GetSpriteByPath("HeroesImages/HeroStats/Knight");
                kd.animator = kd.GameObject.GetComponent<Animator>();
                return kd;
            default:
                return null;
        }

    }
}
