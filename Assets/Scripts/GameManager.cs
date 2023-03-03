using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<PlayerData> _playerDatas = new List<PlayerData>();
    private PlayerInputManager _playerInputManager;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _playerInputManager = GetComponent<PlayerInputManager>();

        _playerInputManager.onPlayerJoined += HandlePlayerJoined;

        SceneManager.sceneLoaded += HandleLoadedScene;
    }

    private void HandleLoadedScene(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.buildIndex == 0)       
            _playerInputManager.joinBehavior = PlayerJoinBehavior.JoinPlayersManually;
        
        else       
            _playerInputManager.joinBehavior= PlayerJoinBehavior.JoinPlayersWhenButtonIsPressed;
        
    }

    private void HandlePlayerJoined(PlayerInput playerInput)
    {
        PlayerData playerData = GetPlayerData(playerInput.playerIndex);

        Player player = playerInput.GetComponent<Player>();
        player.Bind(playerData);
    }

    private PlayerData GetPlayerData(int playerIndex)
    {
        if(_playerDatas.Count <= playerIndex)
        {
            var playerData = new PlayerData();
            _playerDatas.Add(playerData);
        }

        return _playerDatas[playerIndex];
    }
}
