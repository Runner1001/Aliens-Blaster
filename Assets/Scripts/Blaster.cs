using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Blaster : MonoBehaviour
{
    [SerializeField] private GameObject _blasetShotPrefab;
    [SerializeField] private Transform _firePoint;

    private PlayerInput _playerInput;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Fire"].performed += TryFire;
    }

    private void TryFire(InputAction.CallbackContext obj)
    {
        Instantiate(_blasetShotPrefab, _firePoint.position, Quaternion.identity);
    }
}
