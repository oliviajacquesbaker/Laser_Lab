using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelSceneManager
{
    public static int CurrentSceneNumber { get { return CurrentLevelSet.getSceneNumber(CurrentLevelIndex); } }
    public static int CurrentLevelIndex { get; private set; } = -1;
    public static LevelSet CurrentLevelSet { get; private set; }

    public static void LoadLevel(LevelSet set, int index)
    {
        CurrentLevelSet = set;
        CurrentLevelIndex = index;

        if (index >= set.levels.Length)
        {
            Debug.LogWarning("Loading previous level due to invalid level number.");
            LoadLevel(set, index - 1);
            return;
        }
        else if (CurrentSceneNumber >= SceneManager.sceneCountInBuildSettings || CurrentSceneNumber < 4)
        {
            Debug.LogWarning("Loading previous level due to invalid scene number.");
            LoadLevel(set, index - 1);
            return;
        }

        SceneManager.LoadScene(CurrentSceneNumber, LoadSceneMode.Single);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
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
