using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Message : MonoBehaviour, IMessage
{
    protected MessageType type;

    public delegate void MessageClosed(MessageType type);
    public static MessageClosed OnMessageClosed;

    public delegate void MessageClick(MessageType type);
    public static MessageClick OnMessageClick;

    public abstract void CloseBtn();

    public abstract void OnClick();
}
