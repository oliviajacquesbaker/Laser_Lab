using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuFunctions : MonoBehaviour
{
    public LevelSet levelSet;

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void StartGame()
    {
        for (int i = 0; i < levelSet.count - 1; i++) {
            if (PlayerPrefs.HasKey("level complete " + i))
            {
                if (PlayerPrefs.GetInt("level complete " + i) == 0)
                {
                    LevelSceneManager.LoadLevel(levelSet.levels[i]);
                    return;
                }
            }
            else
            {
                PlayerPrefs.SetInt("level complete " + i, 0);
                LevelSceneManager.LoadLevel(levelSet.levels[i]);
                return;
            }
        }

        LevelSceneManager.LoadLevel(levelSet.levels[levelSet.count - 1]);
    }

    public void LoadMenu()
    {
        LevelSceneManager.LoadMenu();
    }

    public void LoadLevelSelect()
    {
        LevelSceneManager.LoadLevelSelect();
    }
}
