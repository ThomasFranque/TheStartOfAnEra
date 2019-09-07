using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    // Properties//
    // Jump height
    [SerializeField] private float Height { get; set; }

    // Check if player is on the ground
    private bool IsGrounded
    {
        get
        {
            Collider2D collider = Physics2D.OverlapCircle(
                transform.position, 2.0f, LayerMask.GetMask("Ground"));
            return (collider != null);
        }
    }

    public override void Update()
    {
        Move();
    }

    // Method for player movement
    public override void Move()
    {
        float hAxis = Input.GetAxis("Horizontal");
        movement = rb.velocity;

        movement = new Vector2(hAxis * MaxSpeed, movement.y);

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
            if (IsGrounded)
            {
                movement.y = Height;
            }
    }

}
