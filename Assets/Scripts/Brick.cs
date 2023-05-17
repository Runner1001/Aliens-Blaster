using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private ParticleSystem _brickParticleEffect;

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
            Instantiate(_brickParticleEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
