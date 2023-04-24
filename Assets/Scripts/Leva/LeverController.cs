using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverController : MonoBehaviour
{
    [SerializeField] Slider sliderHandle;
    [SerializeField] Transform[] positions;
    [SerializeField] float leverSpeed = 5;

    [HideInInspector] public int currentPosition { get; private set; }

    private Coroutine leverCoroutine;

    public void OnValueChange()
    {
        currentPosition = (int)sliderHandle.value;

        if (leverCoroutine != null) StopCoroutine(leverCoroutine);
        leverCoroutine = StartCoroutine(MoveLever(positions[currentPosition].position));
    }

    private void Awake()
    {
        if (positions.Length == 0) Debug.Log("Give me lever positions pls");

        currentPosition = positions.Length - 1;
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
    }
}
