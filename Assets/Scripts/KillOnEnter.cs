using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOnEnter : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        var player = other.collider.GetComponent<Player>();

        if (player)
        {
            player.TakeDamage();
        }
    }
}
