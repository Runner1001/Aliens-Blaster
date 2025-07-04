using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{                                               //this class need a refactor for the SRP
    public event Action OnCoinChanged;
    public event Action OnHealthChanged;

    [SerializeField] private AudioClip _hurtSfx;
    [SerializeField] private float _knockbackForce = 300;


    private Rigidbody2D _rb;
    private AudioSource _audioSource;

    private PlayerData _playerData = new PlayerData();

    public int Coins { get => _playerData.Coins; private set => _playerData.Coins = value; }
    public int Health => _playerData.Health;

    void Awake()
    {
        //FindAnyObjectByType<PlayerCanvas>().Bind(this); //refactor

        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        FindObjectOfType<CinemachineTargetGroup>()?.AddMember(transform, 1f, 1f);
    }

    void OnDisable()
    {
        FindObjectOfType<CinemachineTargetGroup>()?.RemoveMember(transform);
    }

    public void Bind(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public void AddCoin()
    {
        Coins++;
        Debug.Log(_playerData.Coins);
        OnCoinChanged?.Invoke();
    }

    public void TakeDamage(Vector2 hitNormal)
    {
        _playerData.Health--;

        if(_playerData.Health <= 0)
        {
            SceneManager.LoadScene(0);
            return;
        }

        _rb.AddForce(-hitNormal * _knockbackForce);
        _audioSource.PlayOneShot(_hurtSfx);

        OnHealthChanged?.Invoke();
    }

    public void Bounce(Vector2 normal, float bounciness)
    {
        _rb.AddForce(-normal * bounciness);
    }
}
