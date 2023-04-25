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

    public virtual void CloseBtn() 
    {
        OnMessageClosed?.Invoke(type);
    }

    public virtual void OnClick()
    {
        OnMessageClick?.Invoke(type);
    }
}
