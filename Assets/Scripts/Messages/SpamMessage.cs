using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpamMessage : Message
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject btn;

    private readonly int keypadLenght = 4;

    private TextConstructor textConstructor;

    public void SetSpam(MissionScriptable spam)
    {
        image.sprite = spam.missionSprite;
        if (spam.missionText != null)
            textConstructor.InsertParam(spam.missionText, BuildFakeParam(spam.missionType));
        else
            text.text = "";

        btn.SetActive(spam.missionText != null);
    }

    void Awake()
    {
        type = MessageType.Spam;
        textConstructor = new(GameManager.playerName, text);
    }

    string BuildFakeParam(MissionType type)
    {
        switch (type)
        {
            case MissionType.Lever:
                return Random.Range(0, 9).ToString();
            case MissionType.Keypad:
                string s = "";
                for (int i = 0; i < keypadLenght; i++) s += Random.Range(0, 9);
                return s;
            default:
                return "";
        }
    }
}
