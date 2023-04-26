using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WatchController : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    private float elapsed = 0;

    void Update()
    {
        if (GameManager.gameStatus != GameManager.GameStatus.Playing) return;

        elapsed += Time.deltaTime;
        text.text = BuildValue(elapsed, true) + ":" + BuildValue(elapsed, false);
    }

    public string BuildValue(float elapsed, bool minutes)
    {
        int value = minutes ? (int)(elapsed / 60) : (int)elapsed % 60;
        return value < 10 ? $"0{value}" : $"{value}";
    }
    
    public string GetTimerStr(out float sortScore)
    {
        sortScore = elapsed;
        return text.text;
    }

}
