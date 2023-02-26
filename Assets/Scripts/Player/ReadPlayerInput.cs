using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadPlayerInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    private float _horizontal;
    private bool _isJumping;
    private bool _jumpWasPerformed;

    public float Horizontal => _horizontal;
    public bool IsJumping => _isJumping;
    public bool JumpWasPerformed => _jumpWasPerformed;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        _horizontal = _playerInput.actions["Move"].ReadValue<Vector2>().x;

        _isJumping = _playerInput.actions["Jump"].ReadValue<float>() > 0;
        _jumpWasPerformed = _playerInput.actions["Jump"].WasPerformedThisFrame();
    }
}
