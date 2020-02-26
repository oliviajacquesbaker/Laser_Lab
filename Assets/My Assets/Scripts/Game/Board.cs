using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Board
{
    public BoardObject[,] Tiles;
    public WallObject[] Walls;

    public int Width { get { return Tiles.GetLength(0); } }
    public int Height { get { return Tiles.GetLength(1); } }

    public Board(int width, int height)
    {
        Tiles = new BoardObject[width, height];
        Walls = new WallObject[Width * 2 + Height * 2];
        fillDefaultWalls();
    }

    public Board() : this(3, 3) { }

    private void fillDefaultWalls()
    {
        for (int i = 0; i < Walls.Length; i++)
        {
            Walls[i] = new WallObjectBlank();
        }
    }

    public BoardObject GetBoardObject(int x, int y)
    {
        return GetBoardObject(new Vector2Int(x, y));
    }

    public BoardObject GetBoardObject(Vector2Int pos)
    {
        if (pos.x < Width && pos.y < Height)
        {
            return Tiles[pos.x, pos.y];
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

        if (pos.x < 0)
        {
            return Walls[pos.y];
        }
        else if (pos.y >= Height)
        {
            return Walls[Height + pos.x];
        }
        else if (pos.x >= Width)
        {
            return Walls[Height * 2 + Width - pos.y - 1];
        }
        else if (pos.y < 0)
        {
            return Walls[Height * 2 + Width * 2 - pos.x - 1];
        }
        else throw new System.IndexOutOfRangeException("Position " + pos + " is not within the bounds of the walls");
    }

    public ILaserTarget GetLaserTarget(int x, int y)
    {
        return GetLaserTarget(new Vector2Int(x, y));
    }

    public ILaserTarget GetLaserTarget(Vector2Int pos)
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
