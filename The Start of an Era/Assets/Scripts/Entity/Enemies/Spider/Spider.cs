using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
    public override int HP { get; protected set; }

	// ** Sound
	[Header("SoundFX")]
	public AudioClip walk;
    public AudioClip jump;
    public AudioClip alert;

    private float idleStopTime, idleWalkTime;
	private float timeOfIdleStop, timeOfIdleWalk;
	private float jumpSpeed, jumpCooldownTime, timeOfJumpCooldown;

	private int walkDirection;

	private bool InAttackRange
	{
		get
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll
				(transform.position, 30.0f);

			foreach (Collider2D col in colliders)
			{
				if (col.tag == "Player")
					return 1 > 0;
			}

			return false;
		}
	}

	private bool JumpOnCooldown
	{
		get => Time.time < timeOfJumpCooldown + jumpCooldownTime;
	}
	private bool IsIdle
	{
		get => Time.time < idleStopTime + timeOfIdleStop && Time.time > idleStopTime;
	}

	protected override void Awake()
	{
		base.Awake();

		// Starts idle
		idleStopTime = Time.time;
		timeOfIdleStop = 3.0f;
		timeOfIdleWalk = 5.0f;

        //maxSpeed = 85.0f;
        maxSpeed = 0.0f;

        jumpSpeed = 230;
		jumpCooldownTime = 2.0f;
		damage = 1;
		HP = 10;
	}

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //if(HP <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }

    protected override void Move()
	{
        // Blablabla playerScript.position move to that, kill etc

        if (!Knockedback)
        {
            #region Idle Movement
            // check if Stopped
            if (!IsIdle)
            {
                if (Time.time > idleStopTime + timeOfIdleStop)
                {
                    // This will run Once before starting walking
                    idleStopTime = Time.time + timeOfIdleWalk;

                    // Set walking direction
                    walkDirection = Random.Range(0, 101);
                    walkDirection = walkDirection <= 50 ? 1 : -1;
                }
                else
                {
                    // Is walking

                    ////
                    //if (walk.mute)
                    //	StartCoroutine(WalkCoroutine());
                    ////

                    // My solution, maybe works better?
                    // the sound should maybe loop too, and be shorter?
                    if (!audioSrc.isPlaying)
                    {
                        audioSrc.pitch = Random.Range(2.0f, 4.0f);
                        //audioSrc.volume = Random.Range(1.50f, 2.0f);
                        audioSrc.PlayOneShot(walk);

                    }

                    movement = rb.velocity;
                    movement = new Vector2(0.5f * maxSpeed * walkDirection, movement.y);
                    rb.velocity = movement;
                }
            }
            #endregion 
        }
	}

    protected override void OnHit(
        int damage, Vector3 hitDirection, float knockBackSpeed)
    {
        knockbackTimer = 3.0f;

        HP -= damage;
        rb.velocity = hitDirection * knockBackSpeed;
    }

    protected override void WhileIdle()
	{
		Move();
	}

	protected override void OnPlayerSpotted()
	{
		base.OnPlayerSpotted();

		// **Sound Screech here
		audioSrc.pitch = Random.Range(0.75f, 1.5f);
		audioSrc.PlayOneShot(alert);
    }

	protected override void WhileTargetingPlayer()
	{
		movement = rb.velocity;

		if (!InAttackRange)
		{
			// Player on the left
			if (
                transform.position.x >
                targetedPlayerScript.transform.position.x + 20.0f)
			{
				movement = new Vector2(maxSpeed * -1, movement.y);
			}
			// Player on the right 
			else if (
                transform.position.x <
                targetedPlayerScript.transform.position.x - 20.0f)
			{
				movement = new Vector2(maxSpeed, movement.y);
			}

			rb.velocity = movement;
		}
		// jump
		else if (InAttackRange && IsGrounded && !JumpOnCooldown && !Knockedback)
			Jump();
	}

	protected override void Jump()
	{
		// Update time of jump
		timeOfJumpCooldown = Time.time;
		rb.velocity = new Vector2(
			(targetedPlayerScript.transform.position.x - transform.position.x) * (maxSpeed * 0.05f),
			jumpSpeed);

		// Jump Sound
		audioSrc.pitch = Random.Range(2.0f, 2.5f);
		audioSrc.PlayOneShot(jump);
	}

	//   internal IEnumerator WalkCoroutine()
	//   {
	//       walk.mute = false;
	//       walk.pitch = Random.Range(0.75f, 1.5f);
	//       yield return new WaitForSeconds(timeOfIdleWalk);
	//	walk.mute = true;
	//}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, 35.0f);
	}
#endif
}
