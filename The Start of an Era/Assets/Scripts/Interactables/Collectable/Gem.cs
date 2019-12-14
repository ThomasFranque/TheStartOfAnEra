using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Collectable
{
	private int value;
	private SpriteRenderer sr;

	[Header("--- Gem Properties ---")]
	[SerializeField]
	private Sprite lowValueSprt = default;
	[SerializeField]
	private Sprite mediumValueSprt = default;
	[SerializeField]
	private Sprite highValueSprt = default;
	[SerializeField]
	private Sprite legendayValueSprt = default;


	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();

		sr = GetComponent<SpriteRenderer>();

		value = Random.Range(1, 101);

		if (value >= 90)
			sr.sprite = legendayValueSprt;
		else if (value >= 60)
			sr.sprite = highValueSprt;
		else if (value >= 30)
			sr.sprite = mediumValueSprt;
		else
			sr.sprite = lowValueSprt;
	}

	// Update is called once per frame
	void Update()
	{

	}

	protected override void OnPickup()
	{
		Debug.Log($"Picked Up: {name}");
		// GameMngr.Instance.playerMoneyAdd(value);
	}
}
