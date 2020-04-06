using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Game/Level Set")]
public class LevelSet : ScriptableObject
{
    public int ID = 0;
    public int SceneOffset = 4;
    public Level[] levels;
    public int count { get { return levels.Length; } }

    [System.Serializable]
    public class Level
    {
        public string name;
        public Difficulty difficulty;
    }

    public enum Difficulty
    {
        TUTORIAL, BEGINNER, EASY, MEDIUM, HARD, EXPERT, MASTERMIND
    }

    internal int getSceneNumber(int i)
    {
        return i + SceneOffset;
    }
}
