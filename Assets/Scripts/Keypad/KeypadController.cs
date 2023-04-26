using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadController : Mission
{
    [SerializeField] string baseText = "----";
    [SerializeField] TMP_Text text;

    [Header("Audio")]
    [SerializeField] AudioClip successClip;
    [SerializeField] AudioClip failClip;
    public AudioClip digitClip;

    protected override string BuildParam()
    {
        string s = "";
        for (int i = 0; i < baseText.Length; i++) s += Random.Range(0, 9);
        return s;
    }

    protected override bool CheckMissionComplete()
    {
        return text.text == successParam;
    }

    public void UpdateText(int value)
    {
        switch (value)
        {
            case DigitSpecialValues.Enter:
                source.PlayOneShot(CheckMissionComplete() ? successClip : failClip);
                OnMissionComplete?.Invoke(CheckMissionComplete());
                text.text = baseText;
                CustomStopCoroutines();
                break;
            case DigitSpecialValues.Canc:
                ChangeOccurrence("-", false);
                break;
            default:
                ChangeOccurrence(value.ToString(), true);
                break;
        }
    }

    void ChangeOccurrence(string newVal, bool equal)
    {
        string s = text.text;
        int index = s.IndexOf('-');

        if (equal)
        {
            if (index < 0)
                text.text = s.Remove(0, 1).Insert(0, newVal);
            else
                text.text = s.Remove(index, 1).Insert(index, newVal);
        }
        else
        {
            if (index < 0)
                text.text = s.Remove(baseText.Length - 1, 1).Insert(baseText.Length - 1, newVal);
            else if (index > 0)
                text.text = s.Remove(index - 1, 1).Insert(index - 1, newVal);
        }
    }
}
