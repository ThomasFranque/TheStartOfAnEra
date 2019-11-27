using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New attack rune", menuName = "Scriptable Objects/Inventory/Runes")]
public class FireRune : ItemObject
{
    public float attackPower;

    public void Awake()
    {
        type = ItemType.Rune;
    }
}
