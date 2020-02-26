using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level")]
public class Level : ScriptableObject
{
    public Board board;
    public BoardObject[] UnplacedObjects;

    public Level()
    {
        UnplacedObjects = new BoardObject[0];
        board = new Board();
    }
}
