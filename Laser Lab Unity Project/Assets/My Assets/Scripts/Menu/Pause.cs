using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Pause : MonoBehaviour
{
    private static Pause m_current;
    public static Pause Current { get { if (m_current) return m_current; else return null; } }

    public bool paused { get; private set; } = false;

    public GameObject pauseMenu;

    public AudioHitPlayer player;
    public AudioClip pauseSound;
    public AudioClip resumeSound;
    public AudioMixer mixer;
    public AudioMixerSnapshot normalMixerSnapshot;
    public AudioMixerSnapshot pausedMixerSnapshot;

    private void Awake()
    {
        m_current = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            toggle();
        }
    }

    public void pause()
    {
        pause(true);
    }

    public void pause(bool showMenu)
    {
        paused = true;
        pauseMenu.SetActive(showMenu);

        setAudioMode(paused);
    }

    public void resume()
    {
        paused = false;

        if (pauseMenu.activeSelf)
            pauseMenu.SetActive(false);

        setAudioMode(paused);
    }

    public void toggle()
    {
        toggle(true);
    }

    public void toggle(bool showMenu)
    {
        paused = !paused;
        if (showMenu && paused)
            pauseMenu.SetActive(true);

        else if (pauseMenu.activeSelf && !paused)
            pauseMenu.SetActive(false);

        setAudioMode(paused);
    }

    private void setAudioMode(bool paused)
    {
        if (paused)
        {
            player.PlayClip(pauseSound);
            mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { pausedMixerSnapshot }, new float[] { 1 }, .1f);
        } else
        {
            player.PlayClip(resumeSound);
            mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { normalMixerSnapshot }, new float[] { 1 }, 1f);
        }
    }

    private void OnDestroy()
    {
        if (paused)
        {
            mixer.TransitionToSnapshots(new AudioMixerSnapshot[] { normalMixerSnapshot }, new float[] { 1 }, 1f);
        }
    }
}
