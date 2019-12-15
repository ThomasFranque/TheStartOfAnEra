using UnityEngine;
using System;
public class NPCInteract : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Player"))
        {
            OnInteract();
        }
    }

    private void OnInteract()
    {
        interact?.Invoke();
    }

    public Action interact;
}
