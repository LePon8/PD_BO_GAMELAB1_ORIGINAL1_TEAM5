using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMission
{
    [HideInInspector] float Timer { get; set; }

    void StartMission(float Timer);

    void StartAcceptMission(string param);

    void EndMission(bool success);

}
