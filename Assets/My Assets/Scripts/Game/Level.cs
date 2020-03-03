using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Level : MonoBehaviour
{
    public TileSet tileSet;
    public Board board;
    public Floor floor;

    public Vector2Int size { get { return board.Size; } }

    private void OnEnable()
    {
#if UNITY_EDITOR
        Init();
#endif
    }

#if UNITY_EDITOR

    public void Init()
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
    }

    public void Resize(Vector2Int newSize)
    {
        board.Resize(newSize);
        floor.Resize(newSize);
    }

    public void SetTileset(TileSet tileSet)
    {
        this.tileSet = tileSet;
        board.tileSet = tileSet;
        floor.tileSet = tileSet;
    }

#endif
}
