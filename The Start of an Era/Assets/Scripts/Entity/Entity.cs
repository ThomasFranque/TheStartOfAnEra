using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    #region Entity_Variables&Properties
    // Entity variables
    [Header("--- Entity Properties ---")]
    [SerializeField] protected float maxSpeed = default;
    [Tooltip("Layers where player can stand and jump")]
    [SerializeField] protected LayerMask groundLayers = default;
    protected float normalGrav, knockBackSpeed, knockbackTimer;
    protected Vector3 colliderOffset, hitDirection;

    protected Vector2 movement;
    protected Rigidbody2D rb;
    protected AudioSource audioSrc;
    protected bool canJump;

    // Properties//
    public abstract int HP { get; protected set; }

    // Check if player is on the ground
    public bool IsGrounded
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(
                transform.position, 2.0f, groundLayers);
            return (collider != null);
        }
    }

    protected bool Knockedback
    {
        get
        {
            if (knockbackTimer > 0.0f)
            {
                return true;
            }

            return false;
        }
    }
    #endregion
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
        normalGrav = 120.0f;
    }

    public void Hit(int damage, Vector3 hitDirection, float knockBackSpeed)
    {
        OnHit(damage, hitDirection, knockBackSpeed);
    }

    // Method for movement
    protected abstract void Move();

    // Method for taking damage 
    protected abstract void OnHit(int damage, Vector3 hitDirection, float knockBackSpeed);

    // Method for jump action
    protected abstract void Jump();

    // Virtual method to be used if entity has attack actions
    protected virtual void Attack()
    { }
}
