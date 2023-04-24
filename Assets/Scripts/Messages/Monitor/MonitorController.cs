using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite[] spamSprites;
    [SerializeField] Sprite missionSprites;

    [Header("Ref")]
    [SerializeField] SpamMessage spamMessage;
    //[SerializeField] SpamMessage spamMessage;

    private Queue<Sprite> spamQueue = new();
    private Sprite currentSprite;

    private void Awake()
    {
        Message.OnMessageClosed += OnMessageClosed;
        Message.OnMessageClick += OnMessageClick;
    }

    void Start()
    {
        StartSpam();
    }

    void Update()
    {
        
    }

    void OnMessageClosed(MessageType type)
    {
        if (type == MessageType.Mission) return;

        ManageSpamClosed();
    }

    void ManageSpamClosed()
    {
        if(spamQueue.Count == 0)
        {
            spamMessage.gameObject.SetActive(false);
            return;
        }

        currentSprite = spamQueue.Dequeue();
        spamMessage.SetSprite(currentSprite);
    }

    void StartSpam()
    {
        Sprite randomSprite = spamSprites[Random.Range(0, spamSprites.Length)];

        if (!spamMessage.gameObject.activeSelf)
        {
            spamMessage.gameObject.SetActive(true);
        }
        else
        {
            spamQueue.Enqueue(currentSprite);
        }

        currentSprite = randomSprite;
        spamMessage.SetSprite(currentSprite);

    }

    void OnMessageClick(MessageType type)
    {
        if (type == MessageType.Mission) return;

        StartSpam();
    }

    private void OnDestroy()
    {
        Message.OnMessageClosed -= OnMessageClosed;
        Message.OnMessageClick -= OnMessageClick;
    }
}
