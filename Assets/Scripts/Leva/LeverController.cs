using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverController : Mission
{
    [SerializeField] Slider sliderHandle;
    [SerializeField] Transform[] positions;
    [SerializeField] float leverSpeed = 5;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioClip successClip;

    [HideInInspector] public int CurrentPosition { get; private set; }

    private Coroutine leverCoroutine;

    public void OnValueChange()
    {
        CurrentPosition = (int)sliderHandle.value;

        if (leverCoroutine != null) StopCoroutine(leverCoroutine);
        leverCoroutine = StartCoroutine(MoveLever(positions[CurrentPosition].position));
    }

    private void Awake()
    {
        if (positions.Length == 0) Debug.Log("Give me lever positions pls");

        CurrentPosition = positions.Length - 1;
    }

    IEnumerator MoveLever(Vector3 position)
    {
        yield return null;
        while (Vector3.Distance(transform.position, position) >= 0.003f)
        {
            transform.position = Vector3.Lerp(transform.position, position, leverSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = position;
        CommonUtils.ExecuteSound(source, clip);

        if (CheckMissionComplete())
        {
            source.PlayOneShot(successClip);
            OnMissionComplete?.Invoke(true);
            CustomStopCoroutines();
        }

    }

    protected override string BuildParam()
    {
        List<int> possibleValues = new();
        int sliderValue = positions.Length - 1 - (int)sliderHandle.value;
        for (int i = 0; i < positions.Length; i++)
        {
            if (i != sliderValue) possibleValues.Add(i);
        }
        return (possibleValues[Random.Range(1, possibleValues.Count)] + 1).ToString();
    }

    protected override bool CheckMissionComplete()
    {
        return acceptMissionCoroutine != null && positions.Length - (int)sliderHandle.value == int.Parse(successParam);
    }
}
