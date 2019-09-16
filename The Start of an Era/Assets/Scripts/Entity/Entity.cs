using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[Header ("--- Entity Properties ---")]
	// Entity variables
	[SerializeField] protected float maxSpeed;
    [Tooltip ("Layers where player can stand and jump")]
    [SerializeField] LayerMask groundLayers;
	protected Vector3 colliderOffset;
	protected Vector2 movement;
	protected Rigidbody2D rb;
	protected AudioSource audioSrc;
	protected float normalGrav;

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

	protected abstract void Move();
	//movement = rb.velocity;
	//movement = new Vector2(MaxSpeed, 0);

	protected abstract void OnHit(int damage);

    protected abstract void Jump();
}
