using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour, ITakeLaserDamage, ITakeDamage
{
    [SerializeField] private ParticleSystem _brickParticleEffect;
    [SerializeField] private float _laserDestructionTime = 1f;
    [SerializeField] private int _shotsToDestroy = 3;

    private SpriteRenderer _sr;
    private float _damageTaken;
    private float _resetColorTime;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(_resetColorTime > 0 && Time.time >= _resetColorTime)
        {
            _resetColorTime = 0;
            _sr.color = Color.white;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerJump>();

        if (player == null)
            return;

        Vector2 normal = collision.contacts[0].normal;
        float dot = Vector2.Dot(normal, Vector2.up);
        Debug.Log(dot);

        if (dot > 0.5f)
        {
            player.StopJump();
            Explode();
        }
    }

    public void TakeLaserDamage()
    {
        _sr.color = Color.red;
        _resetColorTime = Time.time + 0.1f;

        _damageTaken += Time.deltaTime;
        if (_damageTaken >= _laserDestructionTime)
            Explode();
    }

    private void Explode()
    {
        Instantiate(_brickParticleEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        _damageTaken += _laserDestructionTime / _shotsToDestroy;
        if (_damageTaken >= _laserDestructionTime)
            Explode();
    }
}
