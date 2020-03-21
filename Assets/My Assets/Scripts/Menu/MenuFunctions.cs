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
            if (PlayerPrefs.HasKey("level complete " + levelSet.ID + " " + levelSet.getSceneNumber(i)))
            {
                if (PlayerPrefs.GetInt("level complete " + levelSet.ID + " " + levelSet.getSceneNumber(i)) == 0)
                {
                    LevelSceneManager.LoadLevel(levelSet, i);
                    return;
                }
            }
            else
            {
                PlayerPrefs.SetInt("level complete " + levelSet.ID + " " + levelSet.getSceneNumber(i), 0);
                LevelSceneManager.LoadLevel(levelSet, i);
                return;
            }
        }

        LevelSceneManager.LoadLevel(levelSet, levelSet.count - 1);
    }

    public void LoadMenu()
    {
        LevelSceneManager.LoadMenu();
    }

    public void LoadLevelSelect()
    {
        LevelSceneManager.LoadLevelSelect();
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
