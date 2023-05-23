using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour, ITakeDamage
{
    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }
}
