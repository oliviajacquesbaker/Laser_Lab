﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
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
        int newLevelCount = 3;

        int firstIndex = -1;

        List<LevelSelectObject> tmpList = new List<LevelSelectObject>();

        for (int i = 0; i < set.levels.Length; i++)
        {
            if (set.getSceneNumber(i) >= SceneManager.sceneCountInBuildSettings || set.getSceneNumber(i) < 4)
                continue;

            GameObject newObject = Instantiate(LevelSelectObjectPrefab, transform);
            LevelSelectObject selectObject = newObject.GetComponent<LevelSelectObject>();

            tmpList.Add(selectObject);
            bool complete = true;

            if (!PlayerPrefs.HasKey("level complete " + set.ID + " " + set.getSceneNumber(i)) ||
                PlayerPrefs.GetInt("level complete " + set.ID + " " + set.getSceneNumber(i)) != 1)
            {
                complete = false;
                newLevelCount--;

                if (firstIndex == -1)
                {
                    firstIndex = i;
                }
            }

            selectObject.SetLevel(set.levels[i], i, this, newLevelCount >= 0, complete);
        }

        list = tmpList.ToArray();
        PlayButton.interactable = SelectedLevelIndex >= 0;

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
