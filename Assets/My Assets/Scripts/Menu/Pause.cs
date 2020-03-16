using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private static Pause m_current;
    public static Pause Current { get { if (m_current) return m_current; else return null; } }

    public bool paused { get; private set; } = false;

    public GameObject pauseMenu;

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
    }

    public void resume()
    {
        paused = false;

        if (pauseMenu.activeSelf)
            pauseMenu.SetActive(false);
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
    }
}
