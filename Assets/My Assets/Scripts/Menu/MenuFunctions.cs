using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuFunctions : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

    public void StartGame()
    {
        LevelSceneManager.LoadLevel(null);
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
