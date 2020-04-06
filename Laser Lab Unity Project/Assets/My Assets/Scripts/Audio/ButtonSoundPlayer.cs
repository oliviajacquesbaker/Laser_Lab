using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioHitPlayer))]
public class ButtonSoundPlayer : MonoBehaviour
{
    public AudioClip buttonSound;
    AudioHitPlayer audioHitPlayer;

    private void Awake()
    {
        audioHitPlayer = GetComponent<AudioHitPlayer>();
    }

    public void PlayButtonSound()
    {
        audioHitPlayer.PlayClip(buttonSound);
    }
}
