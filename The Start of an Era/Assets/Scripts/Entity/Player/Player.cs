using System.Collections;
using UnityEngine;

public class Player : Entity
{
    //Player variables
    [SerializeField] private float jumpSpeed;
    private float timeOfJump;
    private float jumpTime;
    private int baseDmg;
    private bool isJumpo;
    [SerializeField] protected LightAttack baseMelee;

    private int runeDmg;
    [Header("Sound")]
    public AudioSource onLand;

	public override int HP { get; protected set; }

    public int ActualDamage { get; private set; }

    protected override void Start()
    {
        base.Start();
        timeOfJump = -1500.0f;
        jumpTime = 0.15f;
        isJumpo = false;
        baseDmg = 6;
        runeDmg = 1;
    }

    protected void Update()
    {
        Move();
        Attack();
        StrenghtCounter();

        //if(HP <= 0)
        //{
        //    Destroy(gameObject);
        //}
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
        }
        // Onland
        else
        {
            timeOfJump = -1500.0f;
            rb.gravityScale = normalGrav;
            isJumpo = false;

            // Onland Sound
            onLand.pitch = Random.Range(1.0f, 1.5f);
            onLand.Play();
        }
    }

    protected override void OnHit(int damage)
    {
        HP -= damage;
    }

    protected override void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            baseMelee.FindTargets();
        }
    }

    protected int StrenghtCounter()
    {
        //Adapt this method after runes are created to calculate how much player can hit enemies with
        return ActualDamage = baseDmg + runeDmg;
    }


    private void OnLand()
    {

    }

    //INSERT INTERACTION METHOD FOR PLAYER TOWARDS WORLD
    //private void Interaction()
    //{

    //}

    protected IEnumerator CWalkingAnim()
    {
        yield return new WaitForSeconds(0.0f);
    }	
}
