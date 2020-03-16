using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public MenuFunctions menuFunctions;
    public GameObject LevelSelectObjectPrefab;

    public int SelectedLevelIndex = -1;

    public LevelSelectObject[] list;

    void Start()
    {
        SelectedLevelIndex = -1;
        LevelSet set = menuFunctions.levelSet;
        int newLevelCount = 3;

        List<LevelSelectObject> tmpList = new List<LevelSelectObject>();

        for (int i = 0; i < set.levels.Length; i++)
        {
            if (set.levels[i].sceneNumber >= SceneManager.sceneCountInBuildSettings || set.levels[i].sceneNumber < 4)
                continue;

            GameObject newObject = Instantiate(LevelSelectObjectPrefab, transform);
            LevelSelectObject selectObject = newObject.GetComponent<LevelSelectObject>();

            tmpList.Add(selectObject);

            if (!PlayerPrefs.HasKey("level complete " + menuFunctions.levelSet.levels[i].sceneNumber) ||
                PlayerPrefs.GetInt("level complete " + menuFunctions.levelSet.levels[i].sceneNumber) != 1)
                newLevelCount--;

            selectObject.SetLevel(set.levels[i], i, this, newLevelCount >= 0);
        }

        list = tmpList.ToArray();
    }

    public void SelectLevel(int index)
    {
        SelectedLevelIndex = index;
        for (int i = 0; i < list.Length;i++)
        {
            list[i].Refresh();
        }
    }

    public void LoadLevel()
    {
        if (SelectedLevelIndex == -1)
            return;
        LevelSceneManager.LoadLevel(menuFunctions.levelSet, SelectedLevelIndex);
    }
}
