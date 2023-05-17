using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _jumpVelocity = 5;
    [SerializeField] private float _jumpDuration = 0.5f;
    [SerializeField] private int maxJumps = 2;

    private Rigidbody2D _rb;
    private AudioSource _audioSource;
    private ReadPlayerInput _readPlayerInput;
    private GroundCheck _groundCheck;
    private PlayerMovement _playerMovement;
    private int _jumpsRemaining;
    private float _jumpEndTime;
    private float _vertical;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _readPlayerInput = GetComponent<ReadPlayerInput>();
        _audioSource = GetComponent<AudioSource>();
        _groundCheck = GetComponent<GroundCheck>();
        _playerMovement = GetComponent<PlayerMovement>();

        _jumpsRemaining = maxJumps;
    }

    void Update()
    {
        Jump();
    }

    private void Jump()
    {
        _vertical = _rb.velocity.y;

        if (_groundCheck.IsGrounded && _rb.velocity.y <= 0)
            _jumpsRemaining = maxJumps;

        if (_readPlayerInput.JumpWasPerformed && _jumpsRemaining > 0)
        {
            _jumpEndTime = Time.time + _jumpDuration;
            _jumpsRemaining--;

            _audioSource.pitch = _jumpsRemaining > 0 ? 1 : 1.2f;
            _audioSource.Play();
        }

        if (_readPlayerInput.IsJumping && _jumpEndTime > Time.time)
            _vertical = _jumpVelocity;

        _rb.velocity = new Vector2(_playerMovement.Horizontal, _vertical);
    }

    public void StopJump()
    {
        _jumpEndTime = Time.time;
    }
}
