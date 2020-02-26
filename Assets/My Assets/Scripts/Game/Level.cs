using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
