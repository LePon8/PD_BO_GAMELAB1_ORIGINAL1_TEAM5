using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public enum MissionType
{
    Lever,
    //Keypad
}

public struct MissionInfo
{
    public MissionType missionType;
    public string param;

    public MissionInfo(MissionType missionType, string param)
    {
        this.missionType = missionType;
        this.param = param;
    }
}

public class MissionMessage : Message
{
    [Header("Refs")]
    [SerializeField] BossMessage bossMessage;
    [SerializeField] TMP_Text missionContent;
    [SerializeField] Image missionImg;
    [SerializeField] LeverController lever;
    [SerializeField] MissionScriptable leverRefs;
    //[SerializeField] KeypadController keypad;

    [Header("Failures")]
    [SerializeField] int maxFailures = 3;
    [SerializeField] float missionTimer = 30;
    [SerializeField] float lowerMissionTimerBound = 15;
    [SerializeField] float decrementValue = 0.3f;

    private int currentFailures = 0;

    private TextConstructor textConstructor;

    public delegate void GameOver();
    public static GameOver OnGameOver;


    private void Awake()
    {
        Mission.OnMissionComplete += OnMissionComplete;
        //textConstructor = new(GameManager.playername, missionContent);
        textConstructor = new("Gianni", missionContent);
        type = MessageType.Mission;
    }

    public bool CheckAvailableMissions(out MissionInfo missionInfo)
    {
        List<MissionInfo> missionTypeList = new();

        Array missionTypeValues = Enum.GetValues(typeof(MissionType));

        foreach (MissionType type in missionTypeValues)
        {
            if (!CheckBusyMission(type, out string param)) missionTypeList.Add(new(type, param));
        }

        if (missionTypeList.Count == 0)
        {
            missionInfo = new(MissionType.Lever, ""); // this wont be used
            return false;
        }

        int randomIndex = UnityEngine.Random.Range(0, missionTypeList.Count);

        missionInfo = missionTypeList[randomIndex];

        return true;
    }

    bool CheckBusyMission(MissionType type, out string param)
    {
        param = "";
        return type switch
        {
            MissionType.Lever => lever.InMission(ref param),
            //MissionType.Keypad => keypad.InMission(ref param),
            _ => true,
        };
    }

    public override void CloseBtn()
    {
        OnMissionComplete(false);
    }

    public override void OnClick()
    {
        base.OnClick();
    }

    public void SetupMission(MissionInfo missionInfo)
    {
        switch (missionInfo.missionType)
        {
            case MissionType.Lever:
                lever.StartMission(missionTimer);
                break;
                //case MissionType.Keypad:
                //    keypad.StartMission(acceptTimer);
                //    break;
        }

        BuildMessage(missionInfo);

        missionTimer = Mathf.Max(lowerMissionTimerBound, missionTimer - decrementValue);
    }

    public void BuildMessage(MissionInfo missionInfo)
    {
        switch (missionInfo.missionType)
        {
            case MissionType.Lever:
                missionImg.sprite = leverRefs.missionSprite;
                textConstructor.InsertParam(leverRefs.missionText, missionInfo.param);
                break;
                //case MissionType.Keypad:
                //    textConstructor.InsertParam(leverText, missionInfo.param);
                //    break;
        }
        
    }

    public void MissionAccepted(MissionInfo missionInfo)
    {
        switch (missionInfo.missionType)
        {
            case MissionType.Lever:
                lever.StartAcceptMission(missionInfo.param);
                break;
                //case MissionType.Keypad:
                //    break;
        }
    }

    void OnMissionComplete(bool success)
    {
        if (!bossMessage.gameObject.activeSelf) bossMessage.gameObject.SetActive(true);

        bossMessage.InsertMessage(success, currentFailures);

        if (!success)
        {
            base.CloseBtn();
            currentFailures++;
            CheckGameEnd();
        }
    }

    private void OnDestroy()
    {
        Mission.OnMissionComplete -= OnMissionComplete;
    }

    void CheckGameEnd()
    {
        if(currentFailures == maxFailures)
        {
            OnGameOver?.Invoke();
        }
    }
}
