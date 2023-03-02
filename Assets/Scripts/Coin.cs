using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _coinClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        var playerWallet = other.GetComponent<PlayerWallet>();

        if (playerWallet)
        {
            GetComponent<AudioSource>().PlayOneShot(_coinClip);
            playerWallet.AddCoin();
            gameObject.SetActive(false);
        }
    }
}
