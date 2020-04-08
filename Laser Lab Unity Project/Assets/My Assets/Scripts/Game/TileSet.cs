using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Tile Set")]
public class TileSet : ScriptableObject
{
    public Environment environment;
    public List<Piece> pieces;

    public Piece FindPieceFromType(Type type)
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].objectType.Equals(type.ToString()))
                return pieces[i];
        }
        return null;
    }

    [Serializable]
    public class Piece
    {
        public string objectType;
        public GameObject Tile;
        public Direction modelOrientation;
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
        public Direction modelOrientation;
    }
}
