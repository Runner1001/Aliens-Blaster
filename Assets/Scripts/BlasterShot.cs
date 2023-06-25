using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BlasterShot : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _maxLifeTime = 3f;

    private Rigidbody2D _rb;
    private Vector2 _direction = Vector2.right;
    private ObjectPool<BlasterShot> _pool;
    private float _selfDestructionTime;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rb.velocity = _direction * _speed;
        if(Time.time >= _selfDestructionTime)
        {
            SelfDestruction();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var damageable = collision.gameObject.GetComponent<ITakeDamage>();
        damageable?.TakeDamage();

        PoolManager.Instance.GetBlasterShotExplosion(collision.contacts[0].point);

        SelfDestruction();
    }

    public void Launch(Vector2 direction, Vector2 position)
    {
        transform.position = position;
        _direction = direction;
        transform.rotation = _direction == Vector2.left ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        _selfDestructionTime = Time.time + _maxLifeTime;
    }

    private void SelfDestruction()
    {
        _pool.Release(this);
    }

    public void SetPool(ObjectPool<BlasterShot> pool)
    {
        _pool = pool;
    }
}
