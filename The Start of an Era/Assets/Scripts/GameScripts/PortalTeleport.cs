﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    void Update()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLLIDE"+ collision);
    }
}
