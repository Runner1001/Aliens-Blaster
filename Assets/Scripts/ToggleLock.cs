using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleLock : MonoBehaviour
{
    [SerializeField] UnityEvent onUnlocked;

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

        if(_unlocked)
            onUnlocked?.Invoke();
    }
}
