using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CC 
{
    private int Duration;
    public Unit Unit;

    public virtual void CreateCC(int moveDuration)
    {
        Duration = moveDuration + GameManager.Instance.numberOfMoves;
    }
    public bool RanOut()
    {
        if (Duration == GameManager.Instance.numberOfMoves)
        {
            RemoveCC();
            return true;
        }
        return false;
    }

    public virtual void RemoveCC()
    {

    }
}

public class Stun : CC
{
    public override void CreateCC(int moveDuration)
    {
        base.CreateCC(moveDuration);
        Unit.currentState = UnitState.Stuned;
    }

    public override void RemoveCC()
    {
        Unit.currentState = UnitState.None;
    }
}

public abstract class BaseAbility
{
    public abstract string Name { get; protected set; }
    public abstract Sprite Sprite { get; protected set; }
    public abstract string Descritption { get; protected set; }
    public abstract AbilityType AbilityType { get; internal set; }

}

public class Passive : BaseAbility
{
    public override string Name { get; protected set; }
    public override Sprite Sprite { get; protected set; }
    public override string Descritption { get; protected set; }
    public override AbilityType AbilityType { get; internal set; } = AbilityType.Passive;
}

public class Ability : BaseAbility
{
    public override string Name { get; protected set; }
    public override Sprite Sprite { get; protected set; }
    public override string Descritption { get; protected set; }
    public override AbilityType AbilityType { get; internal set; }
    public int MaxCooldown;
    public int CoolDown;
    public float Quantity;
    public int Range;
    public Unit CastUnit;


    public virtual void UseAbility(Hex hex) { }

}






public enum AbilityType
{
    Passive = 0,
    Targetable = 1,
    Instant = 2,
}


