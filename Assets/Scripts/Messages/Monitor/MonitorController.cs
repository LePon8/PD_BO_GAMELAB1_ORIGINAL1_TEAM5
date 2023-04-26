using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] float waitTime = 5;
    [SerializeField] float lowerWaitBound = 2;
    [SerializeField] float decrementValue = 0.2f;

    [Header("Sprites")]
    [SerializeField] MissionScriptable[] spamScriptables;

    [Header("Ref")]
    [SerializeField] SpamMessage spamMessage;
    [SerializeField] MissionMessage missionMessage;
    [SerializeField] BossMessage bossMessage;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    private readonly Stack<MissionScriptable> spamQueue = new();
    private MissionScriptable currentSpam;

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
        while (true)
        {
            while (GameManager.gameStatus != GameManager.GameStatus.Playing)
            {
                yield return null;
            }
            if (elapsed >= waitTime)
            {
                CommonUtils.ExecuteSound(source, clip);
                waitTime = Mathf.Max(lowerWaitBound, waitTime - decrementValue);
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

        currentSpam = spamQueue.Pop();
        spamMessage.SetSpam(currentSpam);
    }

    void StartSpam()
    {
        MissionScriptable randomSprite = spamScriptables[Random.Range(0, spamScriptables.Length)];

        if (!spamMessage.gameObject.activeSelf)
        {
            spamMessage.gameObject.SetActive(true);
        }
        else
        {
            spamQueue.Push(currentSpam);
        }

        currentSpam = randomSprite;
        spamMessage.SetSpam(currentSpam);

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
        MissionInfo missionInfo = ManageMissionClosed();
        missionMessage.MissionAccepted(missionInfo);
    }

    MissionInfo ManageMissionClosed()
    {
        MissionInfo missionInfo = missionQueue.Pop();

        if (missionQueue.Count == 0)
        {
            missionMessage.gameObject.SetActive(false);
        }
        else
        {
            MissionInfo[] arr = missionQueue.ToArray();
            missionMessage.BuildMessage(arr[^1]);
        }

        return missionInfo;
    }

}
