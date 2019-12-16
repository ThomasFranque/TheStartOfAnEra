using System.Collections;
using System.Linq;
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
    private int _baseDmg, _runeDmg, _inventoryIndex;
    private bool _isJumpo, _coolDown;

    private HP_Potion[] _inventory;


    // Player properties
    public bool IsInventoryFull
    {
        get
        {
            return _inventory.All(x => x != null);
        }
    }

    public override int HP { get; protected set; }

    public Player Instance { get; private set; }

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
        HP = 50;
        _timeOfJump = -1500.0f;
        _jumpTime = 0.5f;
        _isJumpo = false;
        _baseDmg = 6;
        _runeDmg = 1;
        _lightVelocity = 25.0f;
        _heavyTimer = 2.0f;
        _inventoryIndex = 0;
        _inventory = new HP_Potion[4];

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
            Instance = this;

        DontDestroyOnLoad(Instance);
    }

    protected void Update()
    {
        Move();
        Attack();
        NavigateInventory();

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

    private void NavigateInventory()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _inventoryIndex += 1;

            // Debug
            print(_inventoryIndex);
            print(_inventory[_inventoryIndex]);
            // End debug

            if (_inventoryIndex >= 3)
            {
                _inventoryIndex = 0;
                print(_inventoryIndex);
                print(_inventory[_inventoryIndex]);

            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_inventory[_inventoryIndex] is HP_Potion)
            {
                _inventory[_inventoryIndex].ItemUse();
                _inventory[_inventoryIndex] = null;
            }
        }
    }

    public void InventoryAdd(HP_Potion item)
    {
        for (int i = 0; i < _inventory.Length; i++)
        {
            if (_inventory[i] == null)
            {
                _inventory[i] = item;
                break;
            }
        }
    }

    public void Heal(int heal)
    {
        print($"Hp before heal: {HP}");
 
        if (HP + heal > 100)
        {
            print($"At max health {HP}");

            HP = 100;
        }
        else
        {
            print($"Hp after heal: {HP}");

            HP += heal;
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
