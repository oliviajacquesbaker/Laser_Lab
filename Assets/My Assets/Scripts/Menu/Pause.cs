using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
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
