using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReadPlayerInput : MonoBehaviour
{
    private PlayerInput _playerInput;
    private float _horizontal;

    public float Horizontal => _horizontal;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        _horizontal = _playerInput.actions["Move"].ReadValue<Vector2>().x;
    }
}
