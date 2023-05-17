using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserSwitch : MonoBehaviour
{
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    [SerializeField] private UnityEvent _on;
    [SerializeField] private UnityEvent _off;

    private SpriteRenderer _sr;
    private bool _isOn;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();

        if (player == null)
            return;

        var rb = player.GetComponent<Rigidbody2D>();

        if(rb.velocity.x < 0)
        {
            TurnOff();
        }
        else if(rb.velocity.x > 0)
        {
            TurnOn();
        }
    }

    private void TurnOn()
    {
        if(!_isOn)
        {
            _isOn = true;
            _on.Invoke();
            _sr.sprite = _onSprite;
        }
    }

    private void TurnOff()
    {
        if(_isOn)
        {
            _isOn = false;
            _off.Invoke();
            _sr.sprite = _offSprite;
        }
    }
}
