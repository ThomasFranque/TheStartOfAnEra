using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rune : Interactable
{
    //Rune variables
    protected float normalDmg, elementalDmg;
    protected bool isDiscover, isEquipped;


	// Start is called before the first frame update
	protected override void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	void Update()
    {

	}

	protected override void OnPickup()
	{
		throw new System.NotImplementedException();
	}
}
