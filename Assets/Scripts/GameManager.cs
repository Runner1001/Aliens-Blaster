using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerInputManager _playerInputManager;
    private GameData _gameData;

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
        if(_gameData.PlayerDatas.Count <= playerIndex)
        {
            var playerData = new PlayerData();
            _gameData.PlayerDatas.Add(playerData);
        }

        return _gameData.PlayerDatas[playerIndex];
    }

    public void NewGame()
    {
        _gameData = new GameData(); //a way to reset game data
        SceneManager.LoadScene("Level 1");
    }
}
