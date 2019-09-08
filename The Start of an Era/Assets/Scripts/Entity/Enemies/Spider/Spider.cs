using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy
{
	public override int HP { get; protected set; }

	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	protected override void Update()
	{
		base.Update();
	}

	protected override void Move()
	{
		// Blablabla playerScript.position move to that, kill etc
	}

	protected override void WhileTargetingPlayer()
	{
		Debug.Log("Wtf it works");
	}

	protected override void OnHit(int damage)
	{
		HP -= damage;
	}
}
