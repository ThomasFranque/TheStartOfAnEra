using UnityEngine;

public class RuneAdapter : IAttack
{
    // Instance variables
    protected ItemObject rune;

    public float LightKnockback => throw new System.NotImplementedException();

    public float HeavyKnockback => throw new System.NotImplementedException();

    public Player Player => throw new System.NotImplementedException();

    public Vector2 AttackRange => throw new System.NotImplementedException();
}
