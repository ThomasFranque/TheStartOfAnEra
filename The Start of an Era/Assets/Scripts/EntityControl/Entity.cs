using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Entity variables
    public Rigidbody2D rb;
    public Vector2 movement;

    // Properties//
    // Move speed
    [SerializeField] public float MaxSpeed { get; set; }

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {

    }

    public virtual void Move()
    {
        movement = rb.velocity;

        movement = new Vector2(MaxSpeed, 0);

    }
}
