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
        HP = 10;
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
}
