using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer current;
    [HideInInspector]
    public AudioSource source;

    void Start()
    {
        current = this;
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void PlayClip(AudioClip clip)
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public bool HasClip()
    {
        return source.clip != null;
    }
}
