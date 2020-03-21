using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClipOnLoad : MonoBehaviour
{
    public AudioClip clip;

    void Start()
    {
        if (MusicPlayer.current && (!MusicPlayer.current.HasClip() || !MusicPlayer.current.source.clip.Equals(clip)))
            MusicPlayer.current.PlayClip(clip);
    }
}
