using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	[Header ("--- Entity Properties ---")]
	// Entity variables
	[SerializeField] public float maxSpeed;
	protected Vector3 colliderOffset;
	protected Vector2 movement;
	protected Rigidbody2D rb;

    // Properties//
	public abstract int HP { get; protected set; }

    // Check if player is on the ground
    public bool IsGrounded
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(
                transform.position + colliderOffset, 2.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

	public void Hit(int damage)
	{
		OnHit(damage);
	}

	protected abstract void Move();
	//movement = rb.velocity;
	//movement = new Vector2(MaxSpeed, 0);

	protected abstract void OnHit(int damage);
}
