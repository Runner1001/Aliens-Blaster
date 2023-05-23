using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterShot : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private GameObject _impactExplosionPrefab;

    private Rigidbody2D _rb;
    private Vector2 _direction = Vector2.right;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.velocity = _direction * _speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var damagable = collision.gameObject.GetComponent<ITakeDamage>();
        damagable?.TakeDamage();

        var explosion = Instantiate(_impactExplosionPrefab, collision.contacts[0].point, Quaternion.identity);
        Destroy(explosion.gameObject, 0.8f);

        gameObject.SetActive(false);
    }

    public void Launch(Vector2 direction)
    {
        _direction = direction;
        transform.rotation = _direction == Vector2.left ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
    }
}
