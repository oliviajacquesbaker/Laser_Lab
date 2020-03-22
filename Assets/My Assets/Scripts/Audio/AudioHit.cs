using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public void PlayClip(AudioClip clip)
    {
        source.PlayOneShot(clip);
        hasStarted = true;
    }
}
