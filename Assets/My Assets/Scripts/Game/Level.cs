using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Level : MonoBehaviour
{
    public TileSet tileSet;
    public Board board;
    public Floor floor;
    public BoardObject[] UnplacedObjects;
    public Transform wallsParent, boardObjectParent;

    public Vector2Int size { get { return board.size; } }

    public Level()
    {
        UnplacedObjects = new BoardObject[0];
    }

    private void OnEnable()
    {
        if (!floor)
        {
            GameObject floorObject = new GameObject();
            floor = floorObject.AddComponent<Floor>();
            floorObject.transform.SetParent(transform);
            floor.tileSet = tileSet;
            floorObject.name = "Floor";
        }
        if (!board)
        {
            GameObject floorObject = new GameObject();
            board = floorObject.AddComponent<Board>();
            floorObject.transform.SetParent(transform);
            board.tileSet = tileSet;
            floorObject.name = "Board";
        }
        if (!wallsParent)
        {
            GameObject walls = new GameObject();
            walls.name = "Walls";
            walls.transform.SetParent(transform);
            wallsParent = walls.transform;
        }
        if (!boardObjectParent)
        {
            GameObject objects = new GameObject();
            objects.name = "Objects";
            objects.transform.SetParent(transform);
            boardObjectParent = objects.transform;
        }
    }
}
