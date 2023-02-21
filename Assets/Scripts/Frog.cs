using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float _jumpDelay = 3;
    [SerializeField] private Vector2 _jumpForce;
    [SerializeField] private Sprite _jumpSprite;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private Sprite _idleSprite;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        _idleSprite = _sr.sprite;

        InvokeRepeating("Jump", _jumpDelay, _jumpDelay);
    }

    private void Jump()
    {
        _rb.AddForce(_jumpForce);

        _jumpForce *= new Vector2(-1, 1);
        _sr.flipX = !_sr.flipX;
        _sr.sprite = _jumpSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        _sr.sprite = _idleSprite;
    }
}
