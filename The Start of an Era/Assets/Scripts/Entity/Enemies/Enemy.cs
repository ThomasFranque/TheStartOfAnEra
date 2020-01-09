﻿using UnityEngine;

public abstract class Enemy : Entity
{
	//////////////////////////////////////////////////////////////

	[Header ("--- Enemy Properties ---")]
	[SerializeField]
	private LayerMask sightableLayers = default;

	[SerializeField]
	private GameObject sightStart = default, sightEnd = default;

	[Tooltip ("The time (in seconds) that the enemy will keep interest on the " +
		"player after it has left sight range.")]
	[SerializeField]
	[Range (1, 60)]
	protected float timeOfInterest = default;

	[SerializeField]
	private bool drawLineOfSight = default;

	[SerializeField]
	private Transform _groudCheck;

	//////////////////////////////////////////////////////////////

	protected RaycastHit2D spottedPlayer;
	protected Player targetedPlayerScript;
	protected bool targetingPlayer;
	protected int damage;

	private float spottedTime;

	// Check for the player in sightline
	protected bool IsPlayerEyeSight
	{
		get
		{
			spottedPlayer = Physics2D.Linecast(
				sightStart.transform.position,
				sightEnd.transform.position,
				sightableLayers);

			return spottedPlayer;
		}
	}

	protected bool IsTargeting
	{
		get => targetedPlayerScript != null;
	}

	protected bool HasGround
	{
		get => Physics2D.OverlapCircle(
			_groudCheck.position, 0.20f, LayerMask.GetMask("Ground"));
	}

	// Start is called before the first frame update
	protected override void Awake()
	{
		base.Awake();

		spottedTime = .0f;
		targetingPlayer = false;
	}

    // Update is called once per frame
    protected virtual void Update()
    {
		DoInterestBehaviour();

		if (drawLineOfSight)
			Debug.DrawLine(sightStart.transform.position, 
				sightEnd.transform.position, 
				Color.red);
	}

	protected void DoInterestBehaviour()
	{
		// Check if is targeting something (targetedPlayerScript is null if not)
		if (targetedPlayerScript == null)
		{
			// Player was spotted
			if (IsPlayerEyeSight)
				OnPlayerSpotted();
			else
				WhileIdle();
		}
		// Interest time countdown
		else if (spottedTime + timeOfInterest <= Time.time)
			OnLooseInterest();

		// While the player is in line of sight
		if (IsPlayerEyeSight)
			WhilePlayerInLineOfSight();

		// While the player is being targeted
		if (targetingPlayer)
			WhileTargetingPlayer();
	}

	protected void HitPlayer
        (int damage, Vector3 hitDirection, float knockBackSpeed)
    {
		targetedPlayerScript.Hit(damage, hitDirection, knockBackSpeed);
	}

	protected abstract void WhileIdle();

	protected virtual void OnPlayerSpotted()
	{
		// Swap to get GameManagers Instace of player reference once done
		targetedPlayerScript =
			spottedPlayer.collider.gameObject.GetComponent<Player>();

		targetingPlayer = true;
	}

	protected virtual void WhilePlayerInLineOfSight()
	{
		// If player in line of sight keep updating the spottedTime
		spottedTime = Time.time;		
	}

	protected virtual void OnLooseInterest()
	{
		targetedPlayerScript = null;
		targetingPlayer = false;
	}

	protected abstract void WhileTargetingPlayer();

	protected virtual void OnPlayerCollision(GameObject obj)
	{
		obj.GetComponent<Entity>().Hit(damage, hitDirection, knockBackSpeed);
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		switch (col.gameObject.tag)
		{
			case "Player":
				OnPlayerCollision(col.gameObject);
				break;
		}
	}
}
