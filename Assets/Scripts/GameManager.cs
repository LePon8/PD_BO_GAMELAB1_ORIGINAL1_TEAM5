using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int sceneIndex = 0;

    public static GameStatus gameStatus;
    public static string playerName = "";

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
        gameStatus = GameStatus.GameStart;
        Time.timeScale = 0;
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
        if (gameStatus == GameStatus.GamePause)
        {
            Time.timeScale = 1;
            gameStatus = GameStatus.Playing;
            return;
        }
        Time.timeScale = 0;
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

    public void StartGame()
    {
        gameStatus = GameStatus.Playing;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        gameStatus = GameStatus.GameStart;
        playerName = "";
        SceneManager.LoadScene(sceneIndex);
    }

    public static void SetPlayerName(string name)
    {
        playerName = name;
    }
}
