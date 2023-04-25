using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionRefs", menuName = "ScriptableObjects/MissionRefs")]
public class MissionScriptable : ScriptableObject
{
    public TextAsset missionText;
    public Sprite missionSprite;
}
