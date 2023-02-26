using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private PlayerMovement _playerMovement;
    private PlayerJump _playerJump;
    private GroundCheck _groundCheck;
    private SpriteRenderer _sr;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
        _groundCheck = GetComponent<GroundCheck>();
    }

    void Update()
    {
        UpdateSpriteAndAnimation();
    }

    private void UpdateSpriteAndAnimation()
    {
        _anim.SetBool("IsGrounded", _groundCheck.IsGrounded);
        _anim.SetFloat("HorizontalSpeed", Mathf.Abs(_playerMovement.Horizontal));

        if (_playerMovement.Horizontal > 0)
            _sr.flipX = false;
        else if (_playerMovement.Horizontal < 0)
            _sr.flipX = true;
    }
}
