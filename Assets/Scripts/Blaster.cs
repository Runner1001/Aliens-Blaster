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

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMovement = GetComponent<PlayerMovement>();
        //_playerInput.actions["Fire"].performed += TryFire;
    }

    void Update()
    {
        if(_playerInput.actions["Fire"].ReadValue<float>() > 0)
        {
            BlasterShot shot = PoolManager.Instance.GetBlasterShot();
            shot.Launch(_playerMovement.Direction, _firePoint.position);
        }
    }

    //private void TryFire(InputAction.CallbackContext obj)
    //{
    //    BlasterShot shot = Instantiate(_blasetShotPrefab, _firePoint.position, Quaternion.identity);
    //    shot.Launch(_playerMovement.Direction, _firePoint.position);
    //}
}
