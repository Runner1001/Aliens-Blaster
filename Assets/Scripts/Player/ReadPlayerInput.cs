using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadPlayerInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    private float _horizontal;
    private float _vertical;
    private bool _isJumping;
    private bool _jumpWasPerformed;

    public float Horizontal => _horizontal;
    public float Vertical => _vertical;
    public bool IsJumping => _isJumping;
    public bool JumpWasPerformed => _jumpWasPerformed;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        var input = _playerInput.actions["Move"].ReadValue<Vector2>();
        _horizontal = input.x;
        _vertical = input.y;

        _isJumping = _playerInput.actions["Jump"].ReadValue<float>() > 0;
        _jumpWasPerformed = _playerInput.actions["Jump"].WasPerformedThisFrame();
    }
}
