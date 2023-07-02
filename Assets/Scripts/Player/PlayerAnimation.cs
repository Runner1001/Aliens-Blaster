using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private PlayerMovement _playerMovement;
    private ReadPlayerInput _playerInput;
    private GroundCheck _groundCheck;

    public bool IsDucking { get; private set; }

    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<ReadPlayerInput>();
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
        _anim.SetBool("Duck", _playerInput.Vertical < 0 && Mathf.Abs(_playerInput.Vertical) > Mathf.Abs(_playerMovement.Horizontal));
        IsDucking = _anim.GetBool("IsDucking");
        //_anim.SetFloat("HorizontalSpeed", Mathf.Abs(_playerMovement.Horizontal));

        
    }
}
