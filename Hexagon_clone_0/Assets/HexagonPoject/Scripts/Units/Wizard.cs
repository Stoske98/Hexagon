using System.Collections.Generic;
using UnityEngine;
public class Wizard : Unit
{
    public Wizard():base()
    {
      /*  ability1 = new WizardDarkAbility1
        {
            AbilityType = AbilityType.Targetable,
            Quantity = 1,
            Range = 2,
            MaxCooldown = 4,
            CoolDown = 0,
            CastUnit = this
        };
        ability2 = new WizardDarkAbility2
        {
            AbilityType = AbilityType.Targetable,
            Quantity = 1,
            Range = 2,
            MaxCooldown = 6,
            CoolDown = 0,
            CastUnit = this
        };*/
    }
    public override void Update()
    {
        if (isSpecialMove)
            SpecialMove();
    }

    public override void SpecialMove()
    {
        if (path.Count != 0)
        {
            if (animator.GetBool("Teleport") && Time.time >= currentTime + 1.5f)
            {
                GameObject.transform.position = Map.Instance.map[Column, Row].GameObject.transform.position;
                animator.SetBool("Teleport", false);
                isSpecialMove = false;
                path.Clear();
            }
        }
       
    }

    public override List<Hex> getSpecialMoves(Hex hex)
    {
        specialMoves.Clear();
        specialMoves = PathFinder.BFS_ListInRangeWithNoneWalkableFields(hex,2);
        return specialMoves;
    }

    public override void SetPath(Hex hex)
    {
        path.Add(hex);
        Map.Instance.map[Column, Row].Walkable = true;
        Column = hex.Column;
        Row = hex.Row;
        hex.Walkable = false;
        animator.SetBool("Teleport", true);
        currentTime = Time.time;
    }
}

public class WizardDarkAbility1 : Ability
{
    GameObject TargetAttackVFXprefab;
    GameObject TargetHealkVFXprefab;
    public WizardDarkAbility1(GameObject TargetAttackVFX) : base()
    {
        TargetAttackVFXprefab = TargetAttackVFX;
    }
    public override void UseAbility(Hex hex)
    {
        Debug.Log("USe Q ability");
        Unit unit = Map.Instance.GetUnit(hex);
        
        if (unit.Team != CastUnit.Team)
        {
            if (TargetAttackVFXprefab != null)
            {
                GameManager.Instance.InstantiatePrefabOnPosition(TargetAttackVFXprefab, hex.GameObject.transform.position);
            }
            unit.RecieveDamage((int)Quantity);
        }
        else
            if (unit.MaxHealth >= unit.CurrentHealth + (int)Quantity)
            unit.CurrentHealth += (int)Quantity;
    }
}

public class WizardDarkAbility2 : Ability
{
    GameObject AOEAttackVFXprefab;
    GameObject AOEtHealkVFXprefab;
    public WizardDarkAbility2(GameObject AOEAttackVFX) : base()
    {
        AOEAttackVFXprefab = AOEAttackVFX;
    }
    public override void UseAbility(Hex hex)
    {
        Debug.Log("USe W ability");
        Unit unit = Map.Instance.GetUnit(hex);
       
        if (unit.Team != CastUnit.Team)
        {
            if(AOEAttackVFXprefab!=null)
            {
                GameManager.Instance.InstantiatePrefabOnPosition(AOEAttackVFXprefab, hex.GameObject.transform.position);
            }
            unit.RecieveDamage((int)Quantity);
            Stun stun = new Stun
            {
                Unit = unit

            };
            stun.CreateCC(2);
            CastUnit.cc.Add(stun);
            foreach (var neighbor in hex.neighbors)
            {
                Unit neighborUnit = Map.Instance.GetUnit(neighbor);
                if (neighborUnit != null && neighborUnit.Team != CastUnit.Team)
                    neighborUnit.RecieveDamage((int)(Quantity));
            }

        }
        else
        {
            if (unit.MaxHealth >= unit.CurrentHealth + (int)Quantity)
                unit.CurrentHealth += (int)Quantity;
            foreach (var neighbor in hex.neighbors)
            {
                Unit neighborUnit = Map.Instance.GetUnit(neighbor);
                if (neighborUnit != null && neighborUnit.Team == CastUnit.Team)
                {
                    if (neighborUnit.MaxHealth >= neighborUnit.CurrentHealth + (int)Quantity)
                        neighborUnit.CurrentHealth += (int)Quantity;
                }
            }
        }

    }
}

public class WizardLight : Wizard
{
    public WizardLight() : base()
    {
        ability1 = new WizardDarkAbility1(GameManager.Instance.GetPrefabByPath("WizardAbility/Light/Sunstrike"))
        {
            AbilityType = AbilityType.Targetable,
            Quantity = 1,
            Range = 2,
            MaxCooldown = 4,
            CoolDown = 0,
            CastUnit = this
        };
        ability2 = new WizardDarkAbility2(GameManager.Instance.GetPrefabByPath("WizardAbility/Light/MultipleThunder"))
        {
            AbilityType = AbilityType.Targetable,
            Quantity = 1,
            Range = 2,
            MaxCooldown = 6,
            CoolDown = 0,
            CastUnit = this
        };
    }
}
public class WizardDark : Wizard
{
    public WizardDark() : base()
    {
        ability1 = new WizardDarkAbility1(GameManager.Instance.GetPrefabByPath("WizardAbility/Dark/Sunstrike"))
        {
            AbilityType = AbilityType.Targetable,
            Quantity = 1,
            Range = 2,
            MaxCooldown = 4,
            CoolDown = 0,
            CastUnit = this
        };
        ability2 = new WizardDarkAbility2(GameManager.Instance.GetPrefabByPath("WizardAbility/Dark/MultipleMeteors"))
        {
            AbilityType = AbilityType.Targetable,
            Quantity = 1,
            Range = 2,
            MaxCooldown = 6,
            CoolDown = 0,
            CastUnit = this
        };
    }
}

