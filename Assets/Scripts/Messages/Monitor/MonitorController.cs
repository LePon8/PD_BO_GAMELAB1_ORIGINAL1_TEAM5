using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] float waitTime = 5; 

    [Header("Sprites")]
    [SerializeField] Sprite[] spamSprites;
    [SerializeField] Sprite missionSprites;

    [Header("Ref")]
    [SerializeField] SpamMessage spamMessage;
    //[SerializeField] SpamMessage spamMessage;

    private Stack<Sprite> spamQueue = new();
    private Sprite currentSprite;

    private void Awake()
    {
        Message.OnMessageClosed += OnMessageClosed;
        Message.OnMessageClick += OnMessageClick;
    }

    void Start()
    {
        StartCoroutine(ChooseMessage());
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

        currentSprite = spamQueue.Pop();
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
            spamQueue.Push(currentSprite);
        }

        currentSprite = randomSprite;
        spamMessage.SetSprite(currentSprite);

    }

    IEnumerator ChooseMessage()
    {
        float elapsed = 0;
        //while (GameManager.gameStatus == GameStatus.Playing)
        while (true)
        {
            if(elapsed >= waitTime)
            {
                elapsed = 0;
                //if (Random.value > 0.5f)
                    StartSpam();
                //else
                //    StartMission();

            }
            else
                elapsed += Time.deltaTime;

            yield return null;
        }
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
