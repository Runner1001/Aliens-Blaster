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
    private Animator _anim;
    private ReadPlayerInput _readPlayerInput;
    private GroundCheck _groundCheck;
    private PlayerJump _playerJump;
    private float _horizontal;

    public float Horizontal => _horizontal;
    public Vector2 Direction { get; private set; } = Vector2.right;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _readPlayerInput = GetComponent<ReadPlayerInput>();
        _groundCheck = GetComponent<GroundCheck>();
        _playerJump = GetComponent<PlayerJump>();
    }

    void Update()
    {
        Movement();
        UpdateDirection();
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

    private void UpdateDirection()
    {
        if (_horizontal > 0)
        {
            _anim.transform.rotation = Quaternion.identity;
            Direction = Vector2.right;
        }
        else if (_horizontal < 0)
        {
            _anim.transform.rotation = Quaternion.Euler(0, 180, 0);
            Direction = Vector2.left;
        }
    }
}
