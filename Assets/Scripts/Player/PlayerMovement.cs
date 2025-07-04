using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 5;
    [SerializeField] private float _groundAcceleration = 20;
    [SerializeField] private float _snowAcceleration = 1;
    [SerializeField] private Collider2D _duckCollider;
    [SerializeField] private Collider2D _standingCollider;

    private Rigidbody2D _rb;
    private Animator _anim;
    private ReadPlayerInput _readPlayerInput;
    private GroundCheck _groundCheck;
    private WallCheck _wallCheck;
    private PlayerAnimation _playerAnimation;
    private float _horizontal;

    public float Horizontal => _horizontal;
    public Vector2 Direction { get; private set; } = Vector2.right;
    public bool IsDucking { get; private set; }
    public Collider2D DuckCollider => _duckCollider;
    public Collider2D StandingCollider => _standingCollider;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _readPlayerInput = GetComponent<ReadPlayerInput>();
        _groundCheck = GetComponent<GroundCheck>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _wallCheck = GetComponent<WallCheck>();
    }

    void Update()
    {
        if (GameManager.CinematicPlaying == false)
        {
            Movement();
        }

        UpdateDirection();
    }

    private void Movement()
    {
        var desiredHorizontal = _readPlayerInput.Horizontal * _maxSpeed;
        var acceleration = _groundCheck.IsOnSnow ? _snowAcceleration : _groundAcceleration;

        IsDucking = _playerAnimation.IsDucking;

        if (IsDucking)
            desiredHorizontal = 0;

        _duckCollider.enabled = IsDucking;
        _standingCollider.enabled = !IsDucking;

        //_horizontal = Mathf.Lerp(_horizontal, desiredHorizontal, Time.deltaTime * acceleration);

        if (desiredHorizontal > _horizontal)
        {
            _horizontal += acceleration * Time.deltaTime;
            if (_horizontal > desiredHorizontal)
            {
                _horizontal = desiredHorizontal;
            }
        }
        else if (desiredHorizontal < _horizontal)
        {
            _horizontal -= acceleration * Time.deltaTime;
            if (_horizontal < desiredHorizontal)
            {
                _horizontal = desiredHorizontal;
            }
        }

        if(desiredHorizontal > 0 && _wallCheck.IsTouchingRightWall)
        {
            _horizontal = 0;
        }
        else if(desiredHorizontal < 0 && _wallCheck.IsTouchingLeftWall)
        {
            _horizontal = 0;
        }

        if(_groundCheck.IsInWater)
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y);
        else
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
