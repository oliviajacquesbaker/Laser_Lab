using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Board
{
    [SerializeField]
    private BoardObject[] Tiles;
    [SerializeField]
    private WallObject[] Walls;

    [SerializeField]
    private int m_width;
    [SerializeField]
    private int m_height;

    public int Width { get { return m_width; } }
    public int Height { get { return m_height; } }

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
}
