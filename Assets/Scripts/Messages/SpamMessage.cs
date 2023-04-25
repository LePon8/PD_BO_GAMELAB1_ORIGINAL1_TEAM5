using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpamMessage : Message
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    private TextConstructor textConstructor;

    public void SetSpam(MissionScriptable spam)
    {
        image.sprite = spam.missionSprite;
        if(spam.missionText != null) textConstructor.InsertParam(spam.missionText, BuildParam(spam.missionType));
    }

    void Awake()
    {
        type = MessageType.Spam;
        textConstructor = new("Gianni", text);
    }

    string BuildParam(MissionType type)
    {
        switch (type)
        {
            case MissionType.Lever:
                return Random.Range(0, 9).ToString();
            //case MissionType.Keypad:
            //    string s = "";
            //    for (int i = 0; i < 4; i++) s += Random.Range(0, 9);
            //    return s;
            default:
                return "";
        }
    }

}
