using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyShower : MonoBehaviour
{
    public Text Label;
    public GameObject StarObject;
    public Transform StarParent;

    public void LoadDifficulty(LevelSet.Difficulty difficulty)
    {
        int difficultyNumber = (int)difficulty;
        for (int i = 0; i < difficultyNumber; i++)
        {
            if (difficulty >= LevelSet.Difficulty.SANDBOX)
                break;
            Instantiate(StarObject, StarParent);
        }
        Label.text = difficulty.ToString();
    }

}
