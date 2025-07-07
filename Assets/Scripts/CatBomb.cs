using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CatBomb : MonoBehaviour
{
    [SerializeField] private float _forceAmount = 300f;

    private Rigidbody2D _rb;
    private Animator _anim;
    private ObjectPool<CatBomb> _pool;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.simulated = false;
        _anim = GetComponentInChildren<Animator>();
        _anim.enabled = false;
    }

    public void Lunch(Vector2 direction)
    {
        transform.SetParent(null);
        _rb.simulated = true;
        _rb.AddForce(direction * _forceAmount);
        _anim.enabled = true;
    }

    public void SetPool(ObjectPool<CatBomb> catBombPool)
    {
        _pool = catBombPool;
    }
}
