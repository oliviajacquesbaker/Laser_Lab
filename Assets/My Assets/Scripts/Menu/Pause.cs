using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private static Pause m_current;
    public static Pause Current { get { if (m_current) return m_current; else return null; } }

    private bool m_isPaused = false;

    public bool paused
    {
        get
        {
            return m_isPaused;
        }
        set
        {
            m_isPaused = value;
            pauseMenu.SetActive(value);
        }
    }

    public GameObject pauseMenu;

    private void Start()
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
        paused = true;
    }

    public void resume()
    {
        paused = false;
    }

    public void toggle()
    {
        paused = !paused;
    }
}
