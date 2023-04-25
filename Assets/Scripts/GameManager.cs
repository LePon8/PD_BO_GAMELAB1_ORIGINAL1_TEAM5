using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject pauseMenu;

    private void Awake()
    {
        MissionMessage.OnGameOver += GameOver;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = !pauseMenu.activeSelf ? 0 : 1;
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        }
    }

    void GameOver()
    {
        Debug.Log("you lost");
    }

    private void OnDestroy()
    {
        MissionMessage.OnGameOver -= GameOver;
    }
}
