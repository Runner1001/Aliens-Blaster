using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour, ITakeDamage
{

    void Start()
    {
        var shootAnimationWrapper = GetComponentInChildren<ShootAnimationWrapper>();
        shootAnimationWrapper.OnShoot += Shoot;
    }

    private void Shoot()
    {
        Debug.Log("Shooting");
    }

    public void TakeDamage()
    {
        gameObject.SetActive(false);
    }
}
