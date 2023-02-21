using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float _jumpDelay = 3;
    [SerializeField] private Vector2 _jumpForce;
    [SerializeField] private Sprite _jumpSprite;
    [SerializeField] private int _jumps = 2;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private AudioSource _audioSource;
    private Sprite _idleSprite;
    private int _jumpsRemaining;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();

        _idleSprite = _sr.sprite;
        _jumpsRemaining = _jumps;

        InvokeRepeating("Jump", _jumpDelay, _jumpDelay);
    }

    private void Jump()
    {
        if(_jumpsRemaining <= 0)
        {
            _jumpForce *= new Vector2(-1, 1);
            _jumpsRemaining = _jumps;
        }

        _jumpsRemaining--;
        _rb.AddForce(_jumpForce);

        _sr.flipX = _jumpForce.x > 0;
        _sr.sprite = _jumpSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _sr.sprite = _idleSprite;
        _audioSource.Play();
    }
}
