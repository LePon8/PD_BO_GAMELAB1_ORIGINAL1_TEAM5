using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverController : Mission
{
    [SerializeField] Slider sliderHandle;
    [SerializeField] Transform[] positions;
    [SerializeField] float leverSpeed = 5;

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

        if (CheckMissionComplete())
        {
            OnMissionComplete?.Invoke(true);
            CustomStopCoroutines();
        }

    }

    protected override string BuildParam()
    {
        List<int> possibleValues = new();
        for (int i = 0; i < positions.Length; i++)
        {
            if (i != (int)sliderHandle.value) possibleValues.Add(i);
        }
        return possibleValues[Random.Range(0, possibleValues.Count)].ToString();
    }

    protected override bool CheckMissionComplete()
    {
        return acceptMissionCoroutine != null && (int)sliderHandle.value == int.Parse(successParam);
    }
}
