using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float _jumpDelay = 3;
    [SerializeField] private Vector2 _jumpForce;

    private Rigidbody2D _rb;
    private SpriteRenderer _sr;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();

        InvokeRepeating("Jump", _jumpDelay, _jumpDelay);
    }

    private void Jump()
    {
        _rb.AddForce(_jumpForce);
    }
}
