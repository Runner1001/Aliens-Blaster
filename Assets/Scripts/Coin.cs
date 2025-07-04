using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private AudioClip _coinClip;

    CoinData _data;

    public void Bind(CoinData data)
    {
        _data = data;

        if(data.IsCollected)
            gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerAIO>();

        if (player)
        {
            GetComponent<AudioSource>().PlayOneShot(_coinClip);
            _data.IsCollected = true;
            player.AddPoint();
            gameObject.SetActive(false);
        }
    }
}
