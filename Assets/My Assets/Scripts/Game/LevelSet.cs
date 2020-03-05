using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/Level Set")]
public class LevelSet : ScriptableObject
{
    public Level[] levels;

    [System.Serializable]
    public class Level
    {
        public string name;
        public Difficulty difficulty;
        public Scene scene;
    }

    public enum Difficulty
    {
        BEGINNER, EASY, MEDIUM, HARD, EXPERT, MASTERMIND
    }
}
