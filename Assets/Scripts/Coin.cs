using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _coinClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();

        if (player)
        {
            GetComponent<AudioSource>().PlayOneShot(_coinClip);
            player.AddCoin();
            gameObject.SetActive(false);
        }
    }
}
