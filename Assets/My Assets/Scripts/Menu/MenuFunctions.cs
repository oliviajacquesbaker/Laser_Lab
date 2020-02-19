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

    public void LoadScene(int id)
    {
        LevelSceneManager.loadLevel(id);
    }

    public void LoadScene(string name)
    {
        LevelSceneManager.loadLevel(name);
    }

    public void LoadMenu()
    {
        LevelSceneManager.loadMenu();
    }

    public void LoadLevelSelect()
    {
        LevelSceneManager.loadLevelSelect();
    }
}
