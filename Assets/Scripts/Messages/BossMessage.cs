using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossMessage : Message
{
    [SerializeField] TMP_Text text;
    [SerializeField] TextAsset[] successText;
    [SerializeField] TextAsset[] failText;

    private TextConstructor textConstructor;

    private void Awake()
    {
        type = MessageType.Boss;
        textConstructor = new("Gianni", text);
    }

    public void InsertMessage(bool success, int currentFailure = 0)
    {
        if (success)
        {
            textConstructor.InsertParam(successText[Random.Range(0, successText.Length)], "");
            return;
        }
        textConstructor.InsertParam(failText[currentFailure], "");
    }
}
