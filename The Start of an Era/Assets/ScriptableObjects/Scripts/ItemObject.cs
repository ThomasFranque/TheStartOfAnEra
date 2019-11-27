using UnityEngine;

public enum ItemType
{
    Rune,
    Potion,
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    public new string name;
    [TextArea(15, 20)]
    public string description;

}
