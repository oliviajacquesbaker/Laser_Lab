using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Tile Set")]
public class TileSet : ScriptableObject
{
    public Environment environment;
    public List<Piece> pieces;

    [Serializable]
    public class Piece
    {
        public Type BoardType { get { if (boardObjectScript.GetType().IsSubclassOf(typeof(LaserLabObject))) return boardObjectScript.GetType(); return null; } }
        public MonoBehaviour boardObjectScript;
        public GameObject Tile;
        public Direction defaultOrientation;
    }

    [Serializable]
    public class Environment
    {
        public EnvironmentPiece Corner;
        public EnvironmentPiece Floor;
        public EnvironmentPiece Outside;
    }

    [Serializable]
    public class EnvironmentPiece
    {
        public GameObject Tile;
        public Direction defaultOrientation;
    }
}
