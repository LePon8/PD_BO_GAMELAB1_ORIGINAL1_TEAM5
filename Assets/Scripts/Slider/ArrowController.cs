using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] float changeForceTime = 5;
    [SerializeField] int minForce = -2;
    [SerializeField] int maxForce = 2;
    [SerializeField] float rotationMultiplier = 5;

    [SerializeField] AudioSource source;
    [SerializeField] float triggerRotation = 23.5f;
    [SerializeField] float gameOverRotation = 46f;

    private float elapsed = 0;
    private int currentForce = 0;
    private int knobForce = 0;

    public delegate void GameOver();
    public static GameOver OnGameOver;

    void Update()
    {
        if (GameManager.gameStatus != GameManager.GameStatus.Playing) return;

        float localYRotation = transform.localRotation.eulerAngles.y;
        if (localYRotation > 180) localYRotation -= 360;

        if ((localYRotation <= -triggerRotation || localYRotation >= triggerRotation) && !source.isPlaying) source.Play();

        if(localYRotation <= -gameOverRotation || localYRotation >= gameOverRotation)
        {
            Debug.Log(localYRotation);
            Debug.Log(gameOverRotation);
            source.Stop();
            OnGameOver?.Invoke();
            return;
        }

        if(elapsed < changeForceTime)
        {
            elapsed += Time.deltaTime;
        }
        else
        {
            elapsed = 0;
            ChangeForce();
        }

        int appliedForce = currentForce + knobForce;
        transform.Rotate(Vector3.up, Time.deltaTime * appliedForce * rotationMultiplier);
    }

    void ChangeForce()
    {
        if (currentForce == 0)
            currentForce = Random.value > 0.5f ? Random.Range(minForce, 0) : Random.Range(1, maxForce + 1);
        else if (currentForce > 0)
            currentForce = Random.Range(minForce, 0);
        else
            currentForce = Random.Range(1, maxForce + 1);

    }

    public void UpdateForce(int value)
    {
        knobForce = value;
    }

}
