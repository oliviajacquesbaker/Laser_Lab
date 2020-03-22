using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioHitPlayer : MonoBehaviour
{
    public GameObject audioHitPrefab;
    public AudioMixerGroup audioGroup;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayClip(AudioClip clip)
    {
        GameObject audioHitPrefab = Instantiate(this.audioHitPrefab, transform);
        AudioHit hitPlayer = audioHitPrefab.GetComponent<AudioHit>();
        hitPlayer.PlayClip(clip, audioGroup);
    }
}
