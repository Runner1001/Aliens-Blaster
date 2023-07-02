using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Key : MonoBehaviour, IItem
{
    [SerializeField] private float _useRange = 1.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            playerInventory.PickUp(this);
            
        }
    }

    public void Use()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, _useRange);

        foreach (var hit in hits)
        {
            var toggleLock = hit.GetComponent<ToggleLock>();
            if (toggleLock != null)
            {
                toggleLock.Toggle();
                break;
            }
        }
    }
}
