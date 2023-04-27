using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public struct ScoreTexts
{
    public TMP_Text playerName;
    public TMP_Text score;
}

public class UIManager : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] SaveScriptable saveInfo;
    private readonly string separator = "-";
    [Header("Start")]
    [SerializeField] GameObject startMenu;
    [Header("Name")]
    [SerializeField] GameObject nameMenu;
    [SerializeField] TMP_InputField inputField;
    [Header("Score")]
    [SerializeField] GameObject scoreMenu;
    [SerializeField] ScoreTexts[] scoreTxts;
    [Header("Pause")]
    [SerializeField] GameObject pauseMenu;
    [Header("GameOver")]
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] TMP_Text[] gameOverText;
    [SerializeField] WatchController watch;
    [Header("Audio")]
    [SerializeField] Slider volumeSlider;

    private GameObject currentMenu;

    private void Awake()
    {
        BuildSaveInfo();
    }

    private void Start()
    {
        BuildStartMenu();
        currentMenu = startMenu;
    }

    private void Update()
    {
        if (GameManager.gameStatus == GameManager.GameStatus.GameOver)
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
        if (GameManager.gameStatus == GameManager.GameStatus.GamePause)
            pauseMenu.SetActive(false);
        else if (GameManager.gameStatus == GameManager.GameStatus.Playing)
            pauseMenu.SetActive(true);
    }

    public void StartGame()
    {
        if (inputField.text == "") return;

        GameManager.StartGame();
        GameManager.SetPlayerName(inputField.text);
        currentMenu.SetActive(false);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }

    void BuildEndGame()
    {
        gameOverMenu.SetActive(true);
        string score = watch.GetTimerStr(out float sortScore);
        gameOverText[0].text = GameManager.playerName;
        gameOverText[1].text = score;
        saveInfo.InsertElem(new(GameManager.playerName, score, sortScore));
    }

    void BuildStartMenu()
    {
        SaveInfo[] list = saveInfo.RetrieveList();

        for (int i = 0; i < list.Length; i++)
        {
            scoreTxts[i].playerName.text = list[i].playerName;
            scoreTxts[i].score.text = list[i].score;
        }
    }

    void BuildSaveInfo()
    {
        SaveInfo[] list = saveInfo.RetrieveList();

        if (list.Length != 0 || !PlayerPrefs.HasKey(CommonUtils.PLAYERNAMESKEY)) return;

        string[] playerNames = PlayerPrefs.GetString(CommonUtils.PLAYERNAMESKEY).Split(separator);
        string[] playerScores = PlayerPrefs.GetString(CommonUtils.PLAYERSCORESKEY).Split(separator);

        for (int i = 0; i < playerNames.Length; i++)
        {
            float sortScore = float.Parse(playerScores[i]);
            string score = watch.BuildValue(sortScore, true) + ":" + watch.BuildValue(sortScore, false);
            saveInfo.InsertElem(new(playerNames[i], score, sortScore));
        }
    }

    void BuildPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        SaveInfo[] list = saveInfo.RetrieveList();

        if (list.Length == 0) return;

        string playerNames = list[0].playerName;
        string playerScores = $"{list[0].sortScore}";
        for(int i = 1; i < list.Length; i++)
        {
            playerNames += $"{separator}{list[i].playerName}";
            playerScores += $"{separator}{list[i].sortScore}";
        }

        PlayerPrefs.SetString(CommonUtils.PLAYERNAMESKEY, playerNames);
        PlayerPrefs.SetString(CommonUtils.PLAYERSCORESKEY, playerScores);
    }

    public void Quit()
    {
        BuildPlayerPrefs();
        Application.Quit();
    }

    public void ToStartMenu()
    {
        ChangeMenu(startMenu);
    }

    public void ToNameMenu()
    {
        ChangeMenu(nameMenu);
    }

    public void ToScoreMenu()
    {
        ChangeMenu(scoreMenu);
    }

    void ChangeMenu(GameObject menu)
    {
        currentMenu.SetActive(false);
        currentMenu = menu;
        currentMenu.SetActive(true);
    }

}
