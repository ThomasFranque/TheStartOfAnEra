using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : Collectable
{
	[Header ("--- Heart Container Properties ---")]
	[SerializeField]
	private int healAmmount;

	// When change when gamemanager has the instance player reference
	protected override void OnPickup()
	{
		// When added:
		// GameMngr.Instance.playerScript.Heal(healAmmount);
		Debug.Log($"Picked Up: {name}");
	}
}
