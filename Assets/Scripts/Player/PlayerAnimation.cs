using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private PlayerMovement _playerMovement;
    private GroundCheck _groundCheck;

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _groundCheck = GetComponent<GroundCheck>();
    }

    void Update()
    {
        UpdateSpriteAndAnimation();
    }

    private void UpdateSpriteAndAnimation()
    {
        _anim.SetBool("Jump", !_groundCheck.IsGrounded);
        _anim.SetBool("Move", _playerMovement.Horizontal != 0);
        //_anim.SetFloat("HorizontalSpeed", Mathf.Abs(_playerMovement.Horizontal));

        if (_playerMovement.Horizontal > 0)
            _anim.transform.rotation = Quaternion.identity;
        else if (_playerMovement.Horizontal < 0)
            _anim.transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
