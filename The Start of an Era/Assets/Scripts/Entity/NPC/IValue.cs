﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IValue
{
    // Add a base price variable on the item and multiply it in the get of the price
    int Price { get;}
    int TimesBought { get; set; }
    float PriceModifier { get;  set; }
}
