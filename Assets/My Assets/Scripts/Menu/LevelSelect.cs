using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public MenuFunctions menuFunctions;
    public GameObject LevelSelectObjectPrefab;

    public LevelSet.Level selectedLevel;

    void Start()
    {
        LevelSet set = menuFunctions.levelSet;
        for (int i = 0; i < set.levels.Length; i++)
        {
            GameObject newObject = Instantiate(LevelSelectObjectPrefab, transform);
            LevelSelectObject selectObject = newObject.GetComponent<LevelSelectObject>();
            selectObject.SetLevel(set.levels[i], this);
        }
    }

    public void LoadLevel()
    {
        LevelSceneManager.LoadLevel(selectedLevel);
    }
}
