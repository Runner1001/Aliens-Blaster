using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Blaster : MonoBehaviour
{
    [SerializeField] private BlasterShot _blasetShotPrefab;
    [SerializeField] private Transform _firePoint;

    private PlayerInput _playerInput;
    private PlayerMovement _playerMovement;
    private Animator _anim;

    void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput.actions["Fire"].performed += TryFire;
    }

    void Update()
    {
        //if (_playerInput.actions["Fire"].ReadValue<float>() > 0)
        //    Fire();
    }

    private void TryFire(InputAction.CallbackContext obj)
    {
        Fire();
    }

    private void Fire()
    {
        BlasterShot shot = PoolManager.Instance.GetBlasterShot();
        shot.Launch(_playerMovement.Direction, _firePoint.position);
        _anim.SetTrigger("Fire");
    }
}
