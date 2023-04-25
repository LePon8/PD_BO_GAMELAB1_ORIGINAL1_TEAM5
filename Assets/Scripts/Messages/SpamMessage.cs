using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpamMessage : Message
{
    [SerializeField] Image image;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    void Awake()
    {
        type = MessageType.Spam;
    }

}
