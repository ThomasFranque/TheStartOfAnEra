using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Properties//
    // Jump height
    [SerializeField] private float jumpHeight;

	public override int HP { get; protected set; }

	protected override void Start()
    {
        base.Start();
		// Remove after inserting pivots on sprites
		colliderOffset = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
    }

    private void Update()
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
    private void Jump()
    {
        // Jump only if player is on the ground
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(IsGrounded);

            if (IsGrounded)
            {
                movement.y = jumpHeight;
            }
        }
    }

	protected override void OnHit(int damage)
	{
		HP -= damage;
	}
	
}
