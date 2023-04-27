using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMessage : Message
{
    [SerializeField] Image img;
    [SerializeField] Sprite[] successSprite;
    [SerializeField] Sprite[] failSprite;
    [Header("Audio")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip alarmClip;

    private void Awake()
    {
        type = MessageType.Boss;
    }

    public void InsertMessage(bool success, int currentFailure = 0)
    {
        if (success)
        {
            img.sprite = successSprite[Random.Range(0, successSprite.Length)];
            return;
        }
        CommonUtils.ExecuteSound(source, alarmClip);
        img.sprite = failSprite[currentFailure];
    }
}
