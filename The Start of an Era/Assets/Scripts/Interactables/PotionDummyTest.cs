using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDummyTest : IValue
{
    private int _basePrice;
    private int _timesBought;
    private float _priceModifier;

    public int Price { get => (int)(PriceModifier * _basePrice + _basePrice); }

    public int TimesBought
    {
        get
        {
            return (int)(_timesBought / 5);
        }
        set
        {
            if (value == 0) _timesBought = 0;
            else _timesBought++;
        }
    }

    public float PriceModifier
    {
        get => _priceModifier;
        set
        {
            _priceModifier = value * TimesBought;
        }
    }

    public PotionDummyTest(int price)
    {
        _basePrice = price;
        _timesBought = 0;
    }
}
