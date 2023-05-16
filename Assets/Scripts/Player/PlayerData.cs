using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int Coins = 0;
    public int Health = 8;
}

[Serializable]
public class GameData
{
    public List<PlayerData> PlayerDatas = new List<PlayerData>();
    public string GameName;
}
