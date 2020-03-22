using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectObject : MonoBehaviour
{
    public Color selectedColor = Color.white;
    public Text LevelName;
    public DifficultyShower Difficulty;
    private LevelSelect selector;
    public Button button;
    private LevelSet.Level level;
    private int levelIndex;
    public Image CheckMark;

    bool available = false;

    public void SetLevel(LevelSet.Level level, int levelIndex, LevelSelect selector, bool available, bool complete)
    {
        this.levelIndex = levelIndex;
        this.available = available;
        this.level = level;
        LevelName.text = level.name;
        Difficulty.LoadDifficulty(level.difficulty);

        this.selector = selector;

        if (!available)
            button.interactable = false;
        else
        {
            ColorBlock block = button.colors;
            block.disabledColor = selectedColor;
            button.colors = block;
        }

        CheckMark.gameObject.SetActive(complete);
    }

    public void OnClick()
    {
        selector.SelectLevel(levelIndex);
    }

    public void Refresh()
    {
        if (!available)
            return;
        if (selector.SelectedLevelIndex == levelIndex)
        {
            button.interactable = false;
        }
        else
            button.interactable = true;
    }

}
