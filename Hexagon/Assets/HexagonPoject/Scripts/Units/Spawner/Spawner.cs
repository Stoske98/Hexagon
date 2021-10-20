using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    public virtual Unit spawnUnit(int col, int row, GameObject unitPrefabm, int team) { return null; }

}

//unit.transform.eulerAngles = new Vector3(0, 180, 0);
public class SpawnSwordsman : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team)
    {
        return new Swordsman
        {
            Column = col,
            Row = row,
            GameObject = unitPrefab,

            Team = team,

            MaxHealth = 1000,

            Damage = 1000,
            AttackRange = 1
        };

    }
}

public class SpawnTank : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team)
    {
        return new Tank
        {
            Column = col,
            Row = row,
            GameObject = unitPrefab,

            Team = team,

            MaxHealth = 1000,

            Damage = 1000,
            AttackRange = 1
        };

    }
}

public class SpawnQueen : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team)
    {
        return new Queen
        {
            Column = col,
            Row = row,
            GameObject = unitPrefab,

            Team = team,

            MaxHealth = 1000,

            Damage = 1000,
            AttackRange = 1
        };

    }
}

public class SpawnKing : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team)
    {
        return new King
        {
            Column = col,
            Row = row,
            GameObject = unitPrefab,

            Team = team,

            MaxHealth = 1000,

            Damage = 1000,
            AttackRange = 1
        };

    }
}

public class SpawnArcher : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team)
    {
        return new Archer(GameManager.Instance.projectilPrefab)
        {
            Column = col,
            Row = row,
            GameObject = unitPrefab,

            Team = team,

            MaxHealth = 1000,

            Damage = 1000,
            AttackRange = 1
        };

    }
}

public class SpawnWizard : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team)
    {
        return new Wizard(GameManager.Instance.projectilPrefab)
        {
            Column = col,
            Row = row,
            GameObject = unitPrefab,

            Team = team,

            MaxHealth = 1000,

            Damage = 1000,
            AttackRange = 1
        };

    }
}

public class SpawnTrikset : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team)
    {
        return new Trikster
        {
            Column = col,
            Row = row,
            GameObject = unitPrefab,

            Team = team,

            MaxHealth = 1000,

            Damage = 1000,
            AttackRange = 1
        };

    }
}

public class SpawnKnight : Spawner
{
    public override Unit spawnUnit(int col, int row, GameObject unitPrefab, int team)
    {
        return new Knight
        {
            Column = col,
            Row = row,
            GameObject = unitPrefab,

            Team = team,

            MaxHealth = 1000,

            Damage = 1000,
            AttackRange = 1
        };

    }
}
