using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MissionRefs", menuName = "ScriptableObjects/MissionRefs")]
public class MissionScriptable : ScriptableObject
{
    public MissionType missionType;
    public TextAsset missionText;
    public Sprite missionSprite;
}
