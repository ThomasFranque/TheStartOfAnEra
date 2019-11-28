using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreStock
{
    private float _increasePercentage;
    private int _timesBought;
    private HashSet<IValue> _availableItems;

    public int TimesBought
    {
        get => _timesBought / 5;
        set
        {
            if (value == 0) _timesBought = 0;
            else _timesBought++;
        }
    }

    public StoreStock()
    {
        _timesBought = 0;
        _increasePercentage = 0.0f;
        _availableItems = new HashSet<IValue>();
    }

    // Updates all prices with the times bought
    private void UpdatePrices()
    {
        foreach (IValue v in _availableItems)
        {
            v.PriceModifier = _increasePercentage * TimesBought;
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
    public IEnumerable Items
    {
        get
        {
            foreach (IValue v in _availableItems)
                yield return v;
        }
    }
}
