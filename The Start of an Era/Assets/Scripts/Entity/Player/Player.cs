using UnityEngine;

public class Player : Entity
{
    //Player variables
    [SerializeField] private float jumpSpeed;
    private float timeOfJump;
    private float jumpTime;
    private bool isJumpo;
    

	public override int HP { get; protected set; }

	protected override void Start()
    {
        base.Start();
        timeOfJump = -1500.0f;
        jumpTime = 0.15f;
        isJumpo = false;
    }

    protected void Update()
    {
        Move();
    }

    // Method for player movement
    protected override void Move()
    {
        //Method variables
        float hAxis = Input.GetAxis("Horizontal");

        movement = rb.velocity;

        movement = new Vector2(hAxis * maxSpeed, movement.y);

        // Make player face the right direction
        float shouldInvert = hAxis * transform.right.x;
        if (shouldInvert < 0.0f)
        {
            float yAngle = 180.0f;

            transform.rotation =
                transform.rotation * Quaternion.Euler(0.0f, yAngle, 0.0f);
        }

        Jump();

        rb.velocity = movement;
    }

    // Method for jumping
    protected override void Jump()
    {
        // Jump only if player is on the ground
        if (Input.GetKey(KeyCode.Space))
        {

            if (IsGrounded && !isJumpo)
            {
                rb.gravityScale = normalGrav / 3;
                movement.y = jumpSpeed;
                timeOfJump = Time.time;
                isJumpo = true;
            }

            else if ((Time.time - timeOfJump) < jumpTime && isJumpo)
            {
                rb.gravityScale = normalGrav / 3;
            }

            Debug.Log($"Is on ground:{IsGrounded}");
            Debug.Log($"Is jumping: {isJumpo}");
        }

        else
        {
            timeOfJump = -1500.0f;
            rb.gravityScale = normalGrav;
            isJumpo = false;
        }
    }

    protected override void OnHit(int damage)
	{
		HP -= damage;
	}

    //INSERT INTERACTION METHOD FOR PLAYER TOWARDS WORLD
    //private void Interaction()
    //{

    //}
	
}
