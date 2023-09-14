using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState GameState;

    public static event Action<GameState> GameStateChanged;

    private string _sceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        UpdateGameState(GameState.Hub);
    }

    private void UpdateGameState(GameState state) 
    {
        GameState = state;

        switch (GameState)
        {
            case GameState.Hub:
                HandleHub();
                break;
            case GameState.LoadingMap:
                LoadingMap();
                break;
            case GameState.PlayerTurn:
                HandlePlayerTurn();
                break;
            case GameState.EnemyTurn:
                HandleEnemyTurn();
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }

        GameStateChanged?.Invoke(GameState);
    }

    private async void HandleHub()
    {
        //Toda esta logica deberia estar dentro de un Manager del Hub
        Debug.Log("Cambiando a la escena de partida...");

        await Task.Delay(1000);

        _sceneName = "Escena Principal";

        UpdateGameState(GameState.LoadingMap);
    }

    private async void LoadingMap()
    {
        SceneManager.LoadScene(_sceneName);

        Debug.Log("Cargada nueva escena");

        await Task.Delay(1000);

        UpdateGameState(GameState.PlayerTurn);
    }

    private void HandlePlayerTurn()
    {
        Debug.Log("Tu Turno");
    }

    private void HandleEnemyTurn()
    {
        Debug.Log("Turno de los enemigos");
    }
}

public enum GameState
{
    Hub,
    LoadingMap,
    PlayerTurn,
    EnemyTurn,
    GameOver,
}
