using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : Interactable
{
	private Rigidbody2D rb;

	[Header ("--- Collectable Properties ---")]
	[SerializeField]
	private LayerMask playerLayer;

	protected override void Start()
	{
		base.Start();
		rb = GetComponent<Rigidbody2D>();
		AddSpeed(Random.Range(-25.0f, 25.0f), Random.Range(20.0f, 50.0f));
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player" || col.gameObject.layer == playerLayer)
			OnPickup();
	}

	protected void AddSpeed(float x, float y)
	{
		rb.velocity = new Vector2(x,y);
	}
}
