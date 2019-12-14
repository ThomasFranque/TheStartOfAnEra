using UnityEngine;
public interface IAttack
{
    [SerializeField] float LightKnockback { get; }
    [SerializeField] float HeavyKnockback { get; }
    Player Player { get; }
    Vector2 AttackRange { get; }
}
