using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public event Action OnCoinChanged;
    public event Action OnHealthChanged;

    private PlayerData _playerData = new PlayerData();

    public int Coins { get => _playerData.Coins; private set => _playerData.Coins = value; }
    public int Health => _playerData.Health;

    void Awake()
    {
        FindAnyObjectByType<PlayerCanvas>().Bind(this); //refactor
    }

    public void Bind(PlayerData playerData)
    {
        _playerData = playerData;
    }

    public void AddCoin()
    {
        Coins++;
        OnCoinChanged?.Invoke();
    }

    public void TakeDamage()
    {
        _playerData.Health--;

        if(_playerData.Health <= 0)
        {
            SceneManager.LoadScene(0);
            return;
        }

        OnHealthChanged?.Invoke();
    }
}
