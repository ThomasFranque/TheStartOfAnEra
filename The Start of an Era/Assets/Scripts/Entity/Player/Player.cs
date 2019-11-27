using System.Collections;
using UnityEngine;

public class Player : Entity
{
    // Player variables
    [SerializeField] protected float jumpSpeed = default;

    [Header("Attack transforms")]
    [SerializeField] protected LightAttack LightAttack = default;
    [SerializeField] protected HeavyAttack HeavyAttack = default;

    [Header("Sound")]
    [SerializeField] protected AudioClip landSound = default;

    private float _timeOfJump, _jumpTime, _lightVelocity, _heavyVelocity, _heavyTimer;
    private int _baseDmg, _runeDmg;
    private bool _isJumpo;
    private bool _coolDown;

    // Player properties
    public override int HP { get; protected set; }
    public int ActualDamage
    {
        get => ActualDamage = _baseDmg + _runeDmg;

        private set
        {

        }
    }

    public bool Cooldown
    {
        get
        {
            if (_heavyTimer < 2.0f)
            {
                return true;
            }

            return false;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        HP = 100;
        _timeOfJump = -1500.0f;
        _jumpTime = 0.5f;
        _isJumpo = false;
        _baseDmg = 6;
        _runeDmg = 1;
        _lightVelocity = 25.0f;
        _heavyTimer = 2.0f;
    }

    protected void Update()
    {
        Move();
        Attack();
        Debug.Log(_heavyTimer);


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
            if (IsGrounded && !_isJumpo)
            {
                rb.gravityScale = normalGrav / 3;
                movement.y = jumpSpeed;
                _timeOfJump = Time.time;
                _isJumpo = true;
            }

            else if ((Time.time - _timeOfJump) < _jumpTime && _isJumpo)
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
            _timeOfJump = -1500.0f;
            rb.gravityScale = normalGrav;
            _isJumpo = false;

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
            movement *= _lightVelocity;
            LightAttack.FindTargets();
        }

        else if (!Cooldown)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("Heavy atk");

                HeavyAttack.FindTargets();

                _heavyTimer = 2.0f;
            }
        }
    }

    //INSERT INTERACTION METHOD FOR PLAYER TOWARDS WORLD
    //private void Interaction()
    //{

    //}

    // Co-Routines
    protected IEnumerator CWalkingAnim()
    {
        yield return new WaitForSeconds(0.0f);
    }

    protected IEnumerator CColldown()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
