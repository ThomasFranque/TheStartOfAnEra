using System.Collections;
using UnityEngine;

public class Player : Entity
{
    // Player variables
    [SerializeField]
    protected float jumpSpeed = default, heavyTimer = default;

    [SerializeField] protected LightAttack LightAttack = default;
    [SerializeField] protected HeavyAttack HeavyAttack = default;

    [Header("Sound")]
    [SerializeField] protected AudioClip landSound = default;

    private float timeOfJump, jumpTime, lightVelocity, heavyVelocity;
    private int baseDmg, runeDmg;
    private bool isJumpo;

    // Player properties
	public override int HP { get; protected set; }
    public int ActualDamage
    {
        get => ActualDamage = baseDmg + runeDmg;

        private set
        {

        }
    }

    protected override void Awake()
    {
        base.Awake();
        HP = 100;
        timeOfJump = -1500.0f;
        jumpTime = 0.5f;
        isJumpo = false;
        baseDmg = 6;
        runeDmg = 1;
        lightVelocity = 25.0f;
    }

    protected void Update()
    {
        Move();
        Attack();

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

            else
            {
                rb.gravityScale = normalGrav;
            }
        }

        // Onland
        else
        {
            timeOfJump = -1500.0f;
            rb.gravityScale = normalGrav;
            isJumpo = false;

            // Onland Sound
            audioSrc.pitch = Random.Range(1.0f, 1.5f);
            //audioSrc.PlayOneShot(landSound);
        }
    }

    protected override void OnHit(
        int damage, Vector3 hitDirection, float knockBackSpeed)
    {
        knockbackTimer = 0.5f;

        HP -= damage;
        rb.velocity = knockBackSpeed * hitDirection;
        Debug.Log($"Player's HP: {HP}");
    }

    protected override void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            movement = movement * lightVelocity;
            LightAttack.FindTargets();
        }

        else if(Input.GetKeyDown(KeyCode.X))
        {
            if((Time.time - heavyTimer) < 0.0f)
            {
                HeavyAttack.FindTargets();
            }
        }
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
