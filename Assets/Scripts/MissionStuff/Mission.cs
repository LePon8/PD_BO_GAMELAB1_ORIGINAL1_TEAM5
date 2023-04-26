using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mission : MonoBehaviour, IMission
{
    [SerializeField] public AudioSource source;
    public float Timer { get; set; }

    protected Coroutine missionCoroutine;
    protected Coroutine acceptMissionCoroutine;

    protected string successParam;


    public delegate void MissionComplete(bool success);
    public static MissionComplete OnMissionComplete;

    public bool InMission(ref string param)
    {
        param = BuildParam();
        return missionCoroutine != null || acceptMissionCoroutine != null;
    }

    protected abstract string BuildParam();

    public void EndMission(bool success)
    {
        if (acceptMissionCoroutine != null) OnMissionComplete?.Invoke(success);

        CustomStopCoroutines();
    }

    public void StartMission(float Timer)
    {
        this.Timer = Timer;
        missionCoroutine = StartCoroutine(StartTimer());
    }

    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(Timer);
        EndMission(false);
    }

    public void StartAcceptMission(string param)
    {
        successParam = param;
        if (missionCoroutine != null) StopCoroutine(missionCoroutine);
        acceptMissionCoroutine = StartCoroutine(StartTimer());
    }

    protected abstract bool CheckMissionComplete();

    protected void CustomStopCoroutines()
    {
        acceptMissionCoroutine = null;
        missionCoroutine = null;
        StopAllCoroutines();
    }
}
