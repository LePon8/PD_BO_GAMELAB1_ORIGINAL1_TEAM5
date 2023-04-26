using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Start")]
    [SerializeField] GameObject startMenu;
    [SerializeField] TMP_InputField inputField;
    [Header("Pause")]
    [SerializeField] GameObject pauseMenu;
    [Header("GameOver")]
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] TMP_Text[] gameOverText;
    [SerializeField] WatchController watch;

    private void Update()
    {
        if(GameManager.gameStatus == GameManager.GameStatus.GameOver)
        {
            if (!gameOverMenu.activeSelf)
            {
                BuildEndGame();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if(GameManager.gameStatus == GameManager.GameStatus.GamePause)
        {
            pauseMenu.SetActive(false);
            return;
        }
        pauseMenu.SetActive(true);
    }

    public void StartGame()
    {
        if (inputField.text == "") return;

        GameManager.SetPlayerName(inputField.text);
        startMenu.SetActive(false);
    }

    void BuildEndGame()
    {
        gameOverMenu.SetActive(true);
        gameOverText[0].text = GameManager.playerName;
        gameOverText[1].text = watch.GetTimerStr();
    }

}
