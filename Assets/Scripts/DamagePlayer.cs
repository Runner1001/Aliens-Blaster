using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamagePlayer : MonoBehaviour
{
    [SerializeField] private bool _ignoreFromTop;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (_ignoreFromTop && Vector2.Dot(other.contacts[0].normal, Vector2.down) > 0.5f)
            return;

        var player = other.collider.GetComponent<PlayerAIO>();

        if (player)
        {
            player.TakeDamage(other.contacts[0].normal);
        }
    }
}
