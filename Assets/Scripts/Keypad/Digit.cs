using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digit : MonoBehaviour
{
    [SerializeField] KeypadController keypad;
    [Header("Stat")]
    [SerializeField] float digitSpeed = 3;
    [SerializeField] float finalHeight = 0.007f;
    private float initialHeight;

    private void Awake()
    {
        if (keypad == null) keypad = GetComponentInParent<KeypadController>();    
    }

    void Start()
    {
        initialHeight = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pressed(int value)
    {
        if (value != DigitSpecialValues.Enter) CommonUtils.ExecuteSound(keypad.source, keypad.digitClip);

        keypad.UpdateText(value);
        StartCoroutine(OnPressed());
    }

    IEnumerator OnPressed()
    {
        float currentHeight = transform.localPosition.y;
        while(currentHeight > finalHeight)
        {
            currentHeight = Mathf.MoveTowards(currentHeight, finalHeight, Time.deltaTime * digitSpeed);
            transform.localPosition = new(transform.localPosition.x, currentHeight, transform.localPosition.z);
            yield return null;
        }
        
        while(currentHeight < initialHeight)
        {
            currentHeight = Mathf.MoveTowards(currentHeight, initialHeight, Time.deltaTime * digitSpeed);
            transform.localPosition = new(transform.localPosition.x, currentHeight, transform.localPosition.z);
            yield return null;
        }
    }

}
