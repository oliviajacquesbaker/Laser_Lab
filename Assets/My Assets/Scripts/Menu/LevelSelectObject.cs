using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectObject : MonoBehaviour
{
    public Text LevelName;
    public DifficultyShower Difficulty;
    private LevelSelect selector;
    public Button button;
    private LevelSet.Level level;

    public void SetLevel(LevelSet.Level level, LevelSelect selector)
    {
        this.level = level;
        LevelName.text = level.name;
        Difficulty.LoadDifficulty(level.difficulty);

        this.selector = selector;
    }

    public void OnClick()
    {
        selector.selectedLevel = level;
    }

    private void Update()
    {
        if (selector.selectedLevel == level)
        {
            button.interactable = false;
        }
        else
            button.interactable = true;
    }

}
