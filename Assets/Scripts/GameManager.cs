using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject pauseMenu;

    public static GameStatus gameStatus;

    public enum GameStatus
    {
        GameStart,
        GamePause,
        GameOver,
        Playing
    }

    private void Awake()
    {
        MissionMessage.OnGameOver += GameOver;
        ArrowController.OnGameOver += GameOver;
        gameStatus = GameStatus.Playing;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    void Pause()
    {
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            gameStatus = GameStatus.Playing;
            return;
        }
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        gameStatus = GameStatus.GamePause;
    }

    void GameOver()
    {
        Debug.Log("you lost");
        gameStatus = GameStatus.GameOver;
    }

    private void OnDestroy()
    {
        MissionMessage.OnGameOver -= GameOver;
        ArrowController.OnGameOver -= GameOver;
    }
}
