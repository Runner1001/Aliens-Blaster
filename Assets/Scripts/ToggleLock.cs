using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLock : MonoBehaviour
{
    private bool _unlocked;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = Color.grey;
    }

    [ContextMenu(nameof(Toggle))]
    public void Toggle()
    {
        _unlocked = !_unlocked;
        _sr.color = _unlocked ? Color.white : Color.grey;
    }
}
