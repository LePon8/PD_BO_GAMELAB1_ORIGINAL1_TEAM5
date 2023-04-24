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

    public override void CloseBtn()
    {
        OnMessageClosed?.Invoke(type);
    }

    public override void OnClick()
    {
        OnMessageClick?.Invoke(type);
    }

    // Start is called before the first frame update
    void Start()
    {
        type = MessageType.Spam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
