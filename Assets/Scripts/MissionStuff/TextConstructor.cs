using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextConstructor
{
    string playerName;
    TMP_Text text;

    public TextConstructor(string playerName, TMP_Text text)
    {
        this.playerName = playerName;
        this.text = text;
    }

    public void InsertParam(TextAsset textAsset, string param)
    {
        string myTxt = textAsset.text;
        string outTxt = "";
        foreach(char c in myTxt)
        {
            outTxt += c switch
            {
                '&' => param,
                '£' => playerName,
                _ => c,
            };
        }
        text.text = outTxt;
    }

}
