using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[Header ("--- Entity Properties ---")]
	// Entity variables
	[SerializeField] protected float maxSpeed;
    [Tooltip("Layers where player can stand and jump")]
    [SerializeField] protected LayerMask groundLayers;
    protected float normalGrav;
	protected Vector3 colliderOffset;
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

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSrc = GetComponent<AudioSource>();
        normalGrav = 120.0f;
    }

    public void Hit(int damage)
	{
		OnHit(damage);
	}

    // Method for movement
	protected abstract void Move();
	//movement = rb.velocity;
	//movement = new Vector2(MaxSpeed, 0);
    
    // Method for taking damage 
	protected abstract void OnHit(int damage);

    // Method for jump action
    protected abstract void Jump();

    // Virtual method to be used if entity has attack actions
    protected virtual void Attack()
    { }
}
