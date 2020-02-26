using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelSceneManager
{
    public static Level CurrentLevel { get; private set; }

    public static void LoadLevel(Level level)
    {
        CurrentLevel = level;
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public static void LoadLevelSelect()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
