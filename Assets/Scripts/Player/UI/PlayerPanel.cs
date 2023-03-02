using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private Image[] _hearts;

    private Player _player;

    public void Bind(Player player)
    {
        _player = player;
        _player.OnCoinChanged += UpdateCoins;
        UpdateCoins();
    }

    private void UpdateCoins()
    {
        _coinText.SetText(_player.Coins.ToString());
    }
}
