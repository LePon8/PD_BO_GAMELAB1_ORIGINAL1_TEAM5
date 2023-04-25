using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] float waitTime = 5;

    [Header("Sprites")]
    [SerializeField] Sprite[] spamSprites;

    [Header("Ref")]
    [SerializeField] SpamMessage spamMessage;
    [SerializeField] MissionMessage missionMessage;
    [SerializeField] BossMessage bossMessage;

    private readonly Stack<Sprite> spamQueue = new();
    private Sprite currentSprite;

    private readonly Stack<MissionInfo> missionQueue = new();

    private void Awake()
    {
        Message.OnMessageClosed += OnMessageClosed;
        Message.OnMessageClick += OnMessageClick;
    }

    // SHARED
    void Start()
    {
        StartCoroutine(ChooseMessage());
    }

    void OnMessageClick(MessageType type)
    {
        if (type == MessageType.Mission)
        {
            ManageMissionAccept();
            return;
        }

        StartSpam();
    }

    void OnMessageClosed(MessageType type)
    {
        switch (type)
        {
            case MessageType.Boss:
                bossMessage.gameObject.SetActive(false);
                break;
            case MessageType.Mission:
                ManageMissionClosed();
                break;
            case MessageType.Spam:
                ManageSpamClosed();
                break;
        }
    }

    IEnumerator ChooseMessage()
    {
        float elapsed = 0;
        //while (GameManager.gameStatus == GameStatus.Playing)
        while (true)
        {
            if (elapsed >= waitTime)
            {
                elapsed = 0;
                if (Random.value > 0.5f || !missionMessage.CheckAvailableMissions(out MissionInfo missionInfo))
                    StartSpam();
                else
                    StartMission(missionInfo);
            }
            else
                elapsed += Time.deltaTime;

            yield return null;
        }
    }

    private void OnDestroy()
    {
        Message.OnMessageClosed -= OnMessageClosed;
        Message.OnMessageClick -= OnMessageClick;
    }

    // SPAM
    void ManageSpamClosed()
    {
        if (spamQueue.Count == 0)
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

    // MISSION

    void StartMission(MissionInfo missionInfo)
    {
        if (!missionMessage.gameObject.activeSelf)
        {
            missionMessage.gameObject.SetActive(true);
        }

        missionQueue.Push(missionInfo);

        missionMessage.SetupMission(missionInfo);
    }

    void ManageMissionAccept()
    {
        MissionInfo[] missionInfoArr = missionQueue.ToArray();
        missionMessage.MissionAccepted(missionInfoArr[^1]);
    }

    void ManageMissionClosed()
    {
        missionQueue.Pop();

        if (missionQueue.Count == 0)
        {
            missionMessage.gameObject.SetActive(false);
        }
    }

}
