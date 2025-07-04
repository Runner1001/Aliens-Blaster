using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserSwitch : MonoBehaviour, IBind<LaserSwitchData>
{
    [SerializeField] private Sprite _onSprite;
    [SerializeField] private Sprite _offSprite;

    [SerializeField] private UnityEvent _on;
    [SerializeField] private UnityEvent _off;

    private SpriteRenderer _sr;
    private LaserSwitchData _data;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerAIO>();

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
        if(!_data.IsOn)
        {
            _data.IsOn = true;
            UpdateSwitchState();
        }
    }

    private void TurnOff()
    {
        if(_data.IsOn)
        {
            _data.IsOn = false;
            UpdateSwitchState();
        }
    }

    private void UpdateSwitchState()
    {
        if(_data.IsOn)
        {
            _on.Invoke();
            _sr.sprite = _onSprite;
        }
        else
        {
            _off.Invoke();
            _sr.sprite = _offSprite;
        }          
    }

    public void Bind(LaserSwitchData data)
    {
        _data = data;
        UpdateSwitchState();
    }
}
