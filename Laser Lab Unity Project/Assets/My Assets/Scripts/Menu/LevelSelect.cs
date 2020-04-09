using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public int MaxAvailableLevelsInSection = 3;
    public MenuFunctions menuFunctions;
    public GameObject LevelSelectObjectPrefab;

    public Button PlayButton;

    public ScrollRect scrollRect;

    [HideInInspector]
    public int SelectedLevelIndex = -1;

    [HideInInspector]
    public LevelSelectObject[] list;

    void Start()
    {
        SelectedLevelIndex = -1;
        LevelSet set = menuFunctions.levelSet;
        int newLevelCount = 0;

        int firstIndex = -1;

        List<LevelSelectObject> tmpList = new List<LevelSelectObject>();

        for (int i = 0; i < set.levels.Length; i++)
        {
            if (set.getSceneNumber(i) >= SceneManager.sceneCountInBuildSettings || set.getSceneNumber(i) < 4)
                continue;

            bool complete = true;
            LevelSet.Difficulty difficulty = set.levels[i].difficulty;

            if (!PlayerPrefs.HasKey("level complete " + set.ID + " " + set.getSceneNumber(i)) ||
                PlayerPrefs.GetInt("level complete " + set.ID + " " + set.getSceneNumber(i)) != 1)
            {
                complete = false;

                if (firstIndex == -1)
                {
                    firstIndex = i;
                }
            }

            if (difficulty == LevelSet.Difficulty.TUTORIAL)
            {
                newLevelCount = MaxAvailableLevelsInSection;
                if (!complete)
                    newLevelCount = 1;
            }

            bool levelAvailable = newLevelCount >= 1;

            if (difficulty >= LevelSet.Difficulty.SANDBOX)
                levelAvailable = true;

            if (newLevelCount >= 0 || levelAvailable)
            {

                GameObject newObject = Instantiate(LevelSelectObjectPrefab, transform);
                LevelSelectObject selectObject = newObject.GetComponent<LevelSelectObject>();

                tmpList.Add(selectObject);

                selectObject.SetLevel(set.levels[i], i, this, levelAvailable, complete);
            }

            if (!complete)
            {
                newLevelCount--;
                newLevelCount = Mathf.Min(newLevelCount, MaxAvailableLevelsInSection);
            }
        }

        if (firstIndex == -1)
            firstIndex = set.levels.Length - 1;

        SelectedLevelIndex = firstIndex;

        list = tmpList.ToArray();
        PlayButton.interactable = SelectedLevelIndex >= 0;

        list[SelectedLevelIndex].Refresh();

        scrollRect.verticalNormalizedPosition = Mathf.Clamp(1-(firstIndex * 1f / (set.levels.Length - 2)), 0, 1);
    }

    public void SelectLevel(int index)
    {
        SelectedLevelIndex = index;
        for (int i = 0; i < list.Length;i++)
        {
            list[i].Refresh();
        }
        PlayButton.interactable = SelectedLevelIndex >= 0;
    }

    public void LoadLevel()
    {
        if (SelectedLevelIndex == -1)
            return;
        LevelSceneManager.LoadLevel(menuFunctions.levelSet, SelectedLevelIndex);
    }
}
