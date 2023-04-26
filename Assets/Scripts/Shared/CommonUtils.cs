using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DigitSpecialValues
{
    public const int Canc = -1;
    public const int Enter = 10;
}

public static class CommonUtils
{
    public const string PLAYERNAMESKEY = "PLAYERNAMESKEY";
    public const string PLAYERSCORESKEY = "PLAYERSCORESSKEY";

    public static void ExecuteSound(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null) source.PlayOneShot(clip);
    }
}


