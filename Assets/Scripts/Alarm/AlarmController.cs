using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] AnimationClip clip;
    [SerializeField] float numberOfRotations = 3;

    private Coroutine alarmCoroutine;

    public void StartAlarm()
    {
        if (alarmCoroutine != null)
        {
            anim.SetBool(CommonUtils.alarmBool, false);
            StopCoroutine(alarmCoroutine);
        }
        StartCoroutine(MissionFailAnimation());
    }

    IEnumerator MissionFailAnimation()
    {
        anim.SetBool(CommonUtils.alarmBool, true);
        yield return new WaitForSeconds(clip.length * numberOfRotations);
        anim.SetBool(CommonUtils.alarmBool, false);
    }

}
