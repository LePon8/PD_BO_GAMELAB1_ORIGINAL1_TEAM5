using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageType
{
    Spam,
    Mission
}

public interface IMessage
{
    void CloseBtn();

    void OnClick();
}
