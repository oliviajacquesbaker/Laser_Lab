using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelSceneManager
{
    public static void loadLevel (Scene scene)
    {
        loadLevel(scene.buildIndex);
    }

    public static void loadLevel(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    public static void loadLevel(int id)
    {
        SceneManager.LoadScene(id, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }

    public static void loadMenu ()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
    }
}
