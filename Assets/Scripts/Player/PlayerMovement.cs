using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 5;
    [SerializeField] private float _groundAcceleration = 20;
    [SerializeField] private float _snowAcceleration = 1;

    private Rigidbody2D _rb;
    private ReadPlayerInput _readPlayerInput;
    private GroundCheck _groundCheck;
    private PlayerJump _playerJump;
    private float _horizontal;

    public float Horizontal => _horizontal;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _readPlayerInput = GetComponent<ReadPlayerInput>();
        _groundCheck = GetComponent<GroundCheck>();
        _playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        var desiredHorizontal = _readPlayerInput.Horizontal * _maxSpeed;
        var acceleration = _groundCheck.IsOnSnow ? _snowAcceleration : _groundAcceleration;

        //_horizontal = Mathf.Lerp(_horizontal, desiredHorizontal, Time.deltaTime * acceleration);

        if(desiredHorizontal > _horizontal)
        {
            _horizontal += acceleration * Time.deltaTime;
            if(_horizontal > desiredHorizontal )
            {
                _horizontal = desiredHorizontal;
            }
        }
        else if(desiredHorizontal < _horizontal)
        {
            _horizontal -= acceleration * Time.deltaTime;
            if (_horizontal < desiredHorizontal)
            {
                _horizontal = desiredHorizontal;
            }
        }

        _rb.velocity = new Vector2(_horizontal, _rb.velocity.y);
    }
}
