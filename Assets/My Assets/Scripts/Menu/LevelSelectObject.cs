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

    bool available = false;

    public void SetLevel(LevelSet.Level level, LevelSelect selector, bool available)
    {
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
    }

    public void OnClick()
    {
        selector.SelectLevel(level);
    }

    public void Refresh()
    {
        if (!available)
            return;
        if (selector.selectedLevel == level)
        {
            button.interactable = false;
        }
        else
            button.interactable = true;
    }

}
