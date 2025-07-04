using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int Coins;
    public int Health = 8;
    public Vector2 Position;
    public Vector2 Velocity;
}

[Serializable]
public class GameData
{
    public List<PlayerData> PlayerDatas = new List<PlayerData>();
    public string GameName;
    public string CurrentLevelName;
    public List<LevelData> LevelDatas = new List<LevelData>();
}

[Serializable]
public class LevelData
{
    public string LevelName;
    public List<CoinData> CoinDatas = new List<CoinData>();
    public List<LaserSwitchData> LaserSwitchDatas = new List<LaserSwitchData>();
}

[Serializable]
public class CoinData
{
    public bool IsCollected;
    public string Name;
}

[Serializable]
public class LaserSwitchData
{
    public bool IsOn;
    public string Name;
}