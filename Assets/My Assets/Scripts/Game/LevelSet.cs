using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level Set")]
public class LevelSet : ScriptableObject
{
    public Level[] levels;
}
