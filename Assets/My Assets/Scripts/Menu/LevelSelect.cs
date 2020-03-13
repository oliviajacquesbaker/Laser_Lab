using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public MenuFunctions menuFunctions;
    public GameObject LevelSelectObjectPrefab;

    public LevelSet.Level selectedLevel;

    public LevelSelectObject[] list;

    void Start()
    {
        LevelSet set = menuFunctions.levelSet;
        int newLevelCount = 3;

        List<LevelSelectObject> tmpList = new List<LevelSelectObject>();

        for (int i = 0; i < set.levels.Length; i++)
        {
            GameObject newObject = Instantiate(LevelSelectObjectPrefab, transform);
            LevelSelectObject selectObject = newObject.GetComponent<LevelSelectObject>();

            tmpList.Add(selectObject);

            if (!PlayerPrefs.HasKey("level complete " + menuFunctions.levelSet.levels[i].sceneNumber) ||
                PlayerPrefs.GetInt("level complete " + menuFunctions.levelSet.levels[i].sceneNumber) != 1)
                newLevelCount--;

            selectObject.SetLevel(set.levels[i], this, newLevelCount >= 0);
        }

        list = tmpList.ToArray();
    }

    public void SelectLevel(LevelSet.Level level)
    {
        selectedLevel = level;
        for (int i = 0; i < list.Length;i++)
        {
            list[i].Refresh();
        }
    }

    public void LoadLevel()
    {
        LevelSceneManager.LoadLevel(selectedLevel);
    }
}
