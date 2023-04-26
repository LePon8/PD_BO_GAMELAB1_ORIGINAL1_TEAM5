using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SaveInfo
{
    public string playerName;
    public string score;
    public float sortScore;

    public SaveInfo(string playerName, string score, float sortScore)
    {
        this.playerName = playerName;
        this.score = score;
        this.sortScore = sortScore;
    }
}

[CreateAssetMenu(fileName = "SaveRef", menuName = "ScriptableObjects/SaveObj")]
public class SaveScriptable : ScriptableObject
{
    public int maxSaveElems = 5;
    private readonly List<SaveInfo> saveInfoList = new();

    public SaveInfo[] RetrieveList()
    {
        return saveInfoList.ToArray();
    }

    public void InsertElem(SaveInfo saveInfo)
    {
        saveInfoList.Add(saveInfo);

        saveInfoList.Sort((a,b) => b.sortScore.CompareTo(a.sortScore));

        while(saveInfoList.Count > maxSaveElems)
        {
            saveInfoList.RemoveAt(saveInfoList.Count - 1);
        }
    }

}
