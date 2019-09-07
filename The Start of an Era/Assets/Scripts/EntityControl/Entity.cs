using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Entity variables
    public Rigidbody2D rb;

    protected Vector2 movement;

    [SerializeField] public float maxSpeed;
    
    // To be used by entities in various
    protected Vector3 offset;

    // Properties//

    // Check if player is on the ground
    protected bool IsGrounded
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(
                transform.position + offset, 2.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }
    }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Move()
    {
        //movement = rb.velocity;

        //movement = new Vector2(MaxSpeed, 0);

    }
}
