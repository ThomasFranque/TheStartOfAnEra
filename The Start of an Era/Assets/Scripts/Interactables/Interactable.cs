using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	Collider2D selfCol;

    // Start is called before the first frame update
    protected virtual void Start()
    {
		selfCol = GetComponent<Collider2D>();
    }

	protected abstract void OnPickup();
}
