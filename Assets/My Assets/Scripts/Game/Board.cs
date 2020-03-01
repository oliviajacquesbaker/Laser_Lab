using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    [HideInInspector]
    private BoardObject[] Tiles;
    [SerializeField]
    [HideInInspector]
    private WallObject[] Walls;

    [SerializeField]
    [HideInInspector]
    public TileSet tileSet;

    [SerializeField]
    [HideInInspector]
    private int m_width;
    [SerializeField]
    [HideInInspector]
    private int m_height;

    public int Width { get { return m_width; } }
    public int Height { get { return m_height; } }
    public Vector2Int size { get { return new Vector2Int(Width, Height); } }

    public Board(int width, int height)
    {
        Tiles = new BoardObject[width*height];
        m_height = height;
        m_width = width;
        Walls = new WallObject[Width * 2 + Height * 2];
    }

    public Board() : this(3, 3) { }

    public void SetBoardObject(Vector2Int pos, BoardObject newObject)
    {
        if (IsWithinBoard(pos))
            Tiles[ConvertPositionToBoardCoord(pos)] = newObject;
    }

    public void SetWallObject(Vector2Int pos, WallObject newObject)
    {
        if (IsWallTile(pos))
            Walls[ConvertPositionToWallCoord(pos)] = newObject;
    }

    public int ConvertPositionToWallCoord(Vector2Int pos)
    {
        if (pos.x < 0)
        {
            return pos.y;
        }
        else if (pos.y >= Height)
        {
            return Height + pos.x;
        }
        else if (pos.x >= Width)
        {
            return Height * 2 + Width - pos.y - 1;
        }
        else if (pos.y < 0)
        {
            return Height * 2 + Width * 2 - pos.x - 1;
        }
        return -1;
    }

    public int ConvertPositionToBoardCoord(Vector2Int pos)
    {
        return ConvertPositionToBoardCoord(pos, Width);
    }
    public int ConvertPositionToBoardCoord(Vector2Int pos, int width)
    {
        return pos.x + pos.y * width;
    }

    public BoardObject GetBoardObject(int x, int y)
    {
        return GetBoardObject(new Vector2Int(x, y));
    }

    public BoardObject GetBoardObject(Vector2Int pos)
    {
        if (pos.x < Width && pos.y < Height)
        {
            return Tiles[ConvertPositionToBoardCoord(pos)];
        }
        else throw new System.IndexOutOfRangeException("Position " + pos + " is outside the bounds of the board");
    }

    public WallObject GetWallObject(int x, int y)
    {
        return GetWallObject(new Vector2Int(x, y));
    }

    public WallObject GetWallObject(Vector2Int pos)
    {
        if (!IsWallTile(pos))
            return null;

        int wallCoord = ConvertPositionToWallCoord(pos);
        if (wallCoord >= 0)
            return Walls[wallCoord];
        else throw new System.IndexOutOfRangeException("Position " + pos + " is not within the bounds of the walls");
    }

    public LaserLabObject GetLaserLabObject(int x, int y)
    {
        return GetLaserLabObject(new Vector2Int(x, y));
    }

    public LaserLabObject GetLaserLabObject(Vector2Int pos)
    {
        if (IsWithinBoard(pos))
        {
            return GetBoardObject(pos);
        }
        else if (IsWithinWalls(pos))
        {
            return GetWallObject(pos);
        }
        else throw new System.IndexOutOfRangeException("Position " + pos + " is outside the bounds of the walls");
    }

    public bool IsWithinBoard(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < Width && pos.y < Height;
    }

    public bool IsWithinWalls(Vector2Int pos)
    {
        if (pos.x < 0 && pos.y < 0 || pos.x >= Width && pos.y < 0 || pos.x < 0 && pos.y >= Height || pos.x >= Width && pos.y >= Height)
            return false;
        return pos.x >= -1 && pos.y >= -1 && pos.x <= Width && pos.y <= Height;
    }

    public bool IsWallTile(Vector2Int pos)
    {
        return !IsWithinBoard(pos) && IsWithinWalls(pos);
    }
#if UNITY_EDITOR
    public void ReloadTiles()
    {

    }

    public void Resize(Vector2Int newSize)
    {
        Board tmpBoard = gameObject.AddComponent<Board>();
        tmpBoard.m_width = newSize.x;
        tmpBoard.m_height = newSize.y;
        tmpBoard.tileSet = tileSet;

        for (int i = -1; i <= tmpBoard.Width || i <= Width; i++)
        {
            for (int j = -1; j <= tmpBoard.Height || j <= Height; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                if (IsWithinBoard(pos))
                {
                    BoardObject obj = GetBoardObject(pos);
                    if (obj) {
                        if (tmpBoard.IsWithinBoard(pos))
                        {
                            tmpBoard.SetBoardObject(pos, obj);
                        }
                        else
                        {
                            DestroyImmediate(obj.gameObject);
                        }
                    }
                }

                if (IsWithinWalls(pos))
                {
                    if ((i > tmpBoard.Width   &&  (j == -1 || j == Height)) || 
                        (j > tmpBoard.Height  &&  (i == -1 || i == Width)))
                    {
                        DestroyImmediate(GetWallObject(pos).gameObject);
                    }
                }

                if (tmpBoard.IsWithinWalls(pos))
                {
                    if ((i > Width && (j == -1 || j == tmpBoard.Height)) ||
                        (j > Height && (i == -1 || i == tmpBoard.Width)))
                    {
                        tmpBoard.CreateWallAt(pos);
                    }
                }
            }
        }

        Walls = tmpBoard.Walls;
        Tiles = tmpBoard.Tiles;
        m_width = tmpBoard.Width;
        m_height = tmpBoard.Height;

        DestroyImmediate(tmpBoard);
    }

    private void CreateWallAt(Vector2Int pos)
    {
        TileSet.Piece piece = tileSet.FindPieceFromType(typeof(WallObjectBlank));
        GameObject newWallObject = Instantiate(piece.Tile);
        newWallObject.name = "Wall (" + pos.x + "," + pos.y + ")";
        WallObject wallObject = newWallObject.AddComponent(typeof(WallObjectBlank)) as WallObject;
        newWallObject.transform.SetParent(transform);
        newWallObject.transform.position = new Vector3(pos.x, 0, pos.y);
        Direction wallDirection = GetWallOrientation(pos);
        newWallObject.transform.rotation = Quaternion.Euler(0, (-90 * (int)piece.modelOrientation) + 90 * (int)wallDirection, 0);
    }

    private Direction GetWallOrientation(Vector2Int pos)
    {
        if (pos.x == -1)
            return Direction.RIGHT;
        if (pos.y == -1)
            return Direction.UP;
        if (pos.x == Width)
            return Direction.LEFT;
        if (pos.y == Height)
            return Direction.DOWN;
        return Direction.UP;
    }

    private Vector2Int corrolateWallForward(Vector2Int pos, Vector2Int size1, Vector2Int size2)
    {
        if ((pos.x == -1 || pos.y == -1) && pos.y != pos.x)
        {
            if (pos.x < size2.x && pos.y < size2.y)
                return pos;
            else return new Vector2Int(-1, -1);
        }
        else if (pos.x == size1.x)
        {
            if (pos.y < size2.y)
                return new Vector2Int(size2.x, pos.y);
            else return new Vector2Int(-1, -1);
        }
        else if (pos.y == size1.y)
        {
            if (pos.x < size2.x)
                return new Vector2Int(pos.x, size2.y);
            else return new Vector2Int(-1, -1);
        }

        return new Vector2Int(-1, -1);
    }
#endif
}
