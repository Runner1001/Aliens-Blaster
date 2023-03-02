using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    PlayerData playerData = new PlayerData();

    public int Coins { get => playerData.Coins; private set => playerData.Coins = value; }

    public void AddCoin()
    {
        Coins++;
    }
}
