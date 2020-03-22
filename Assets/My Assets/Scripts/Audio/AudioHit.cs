using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioHit : MonoBehaviour
{
    AudioSource source;
    bool hasStarted = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (hasStarted && !source.isPlaying)
            Destroy(gameObject);
    }

    public void PlayClip(AudioClip clip, AudioMixerGroup audioGroup = null)
    {
        DontDestroyOnLoad(gameObject);
        source.outputAudioMixerGroup = audioGroup;
        source.PlayOneShot(clip);
        hasStarted = true;
    }
}
