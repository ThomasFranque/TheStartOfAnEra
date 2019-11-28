using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDummyTest : IValue
{
    private int _basePrice;
    public int Price { get => (int)(PriceModifier * _basePrice + _basePrice); }
    public float PriceModifier { get;  set; }

    public PotionDummyTest(int price)
    {
        _basePrice = price;
    }
}
