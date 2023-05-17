using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ParticleSystem _brickParticleEffect;
    [SerializeField] private float _laserDestructionTime = 1f;

    private float _damegeTaken;

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
        _damegeTaken += Time.deltaTime;
        if (_damegeTaken >= _laserDestructionTime)
            Explode();
    }

    private void Explode()
    {
        Instantiate(_brickParticleEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
