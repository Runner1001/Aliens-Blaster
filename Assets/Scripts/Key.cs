using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    [SerializeField] private float _useRange = 1.0f;

    public override void Use()
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
