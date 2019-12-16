using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreStock
{
    private float _increasePercentage;
    private HashSet<IValue> _availableItems;

    public StoreStock()
    {
        _increasePercentage = 0.05f;
        _availableItems = new HashSet<IValue>();
    }

    // Updates all prices with the times bought
    public void UpdatePrices()
    {
        foreach (IValue v in _availableItems)
        {
            v.PriceModifier = _increasePercentage;
        }
    }

    public void AddStock(IValue item)
    {
        _availableItems.Add(item);
    }

    public void RemoveStock(IValue item)
    {
        _availableItems.Remove(item);
    }

    // Returns all items in stock
    public IEnumerable<IValue> Items
    {
        get
        {
            return _availableItems;
        }
    }
}
