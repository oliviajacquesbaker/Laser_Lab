using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/Level Set")]
public class LevelSet : ScriptableObject
{
    public Level[] levels;
    public int count { get { return levels.Length; } }

    [System.Serializable]
    public class Level
    {
        public string name;
        public Difficulty difficulty;
        public int sceneNumber = 4;
    }

    public enum Difficulty
    {
        TUTORIAL, BEGINNER, EASY, MEDIUM, HARD, EXPERT, MASTERMIND
    }
}
