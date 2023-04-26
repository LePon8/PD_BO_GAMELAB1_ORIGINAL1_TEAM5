using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] ArrowController arrow;
    [SerializeField] float knobSpeed = 5;
    [SerializeField] Transform[] knobPositions;
    [SerializeField] int minRange = -3;
    [SerializeField] int maxRange = 3;

    [Header("Audio")]
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    private Coroutine knobCoroutine;

    private int currentPosition;

    public void OnValueChange()
    {
        arrow.UpdateForce((int)slider.value);

        currentPosition = (int)slider.value + maxRange;

        if (knobCoroutine != null) StopCoroutine(knobCoroutine);
        knobCoroutine = StartCoroutine(MoveKnob(knobPositions[currentPosition].position));
    }

    void Start()
    {
        currentPosition = (int)slider.value + maxRange;
        transform.position = knobPositions[currentPosition].position;
    }

    void Update()
    {
        
    }

    IEnumerator MoveKnob(Vector3 position)
    {
        yield return null;
        while (Vector3.Distance(transform.position, position) >= 0.003f)
        {
            transform.position = Vector3.Lerp(transform.position, position, knobSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = position;
        CommonUtils.ExecuteSound(source, clip);
    }
}
