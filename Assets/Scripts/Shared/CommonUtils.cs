using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonUtils
{
    public static void ExecuteSound(AudioSource source, AudioClip clip)
    {
        if (source != null && clip != null) source.PlayOneShot(clip);
    }
}
