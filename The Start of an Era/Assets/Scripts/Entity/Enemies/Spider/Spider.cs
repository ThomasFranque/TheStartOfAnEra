using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
	public override int HP { get; protected set; }

	// ** Sound
	[Header("SoundFX")]
	public AudioSource walk;

	private float idleStopTime, idleWalkTime, idleWalkSpeed;
	private float timeOfIdleStop, timeOfIdleWalk;

	private int walkDirection;

	private bool IsIdle
	{
		get => Time.time < idleStopTime + timeOfIdleStop && Time.time > idleStopTime;
	}

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();

		// Starts idle
		idleStopTime = Time.time;
		timeOfIdleStop = 3.0f;
		timeOfIdleWalk = 5.0f;

		maxSpeed = 100;
		HP = 1;
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	protected override void Move()
	{
		// Blablabla playerScript.position move to that, kill etc

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
				if (!walk.isPlaying)
				{
					walk.pitch = Random.Range(0.75f, 1.5f);
					walk.Play();
				}

				movement = rb.velocity;
				movement = new Vector2(0.5f * maxSpeed * walkDirection, movement.y);
				rb.velocity = movement;
			}
		}
		#endregion
	}

	protected override void WhileIdle()
	{
		Move();
	}

	protected override void OnPlayerSpotted()
	{
		base.OnPlayerSpotted();
	}

	protected override void WhileTargetingPlayer()
	{
		//transform.Translate(targetedPlayerScript.transform.position * movement);
	}

	protected override void OnHit(int damage)
	{
		HP -= damage;
	}

	protected override void Jump()
	{
		throw new System.NotImplementedException();
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
		Gizmos.DrawSphere(transform.position, 2.0f);
	}
#endif
}
