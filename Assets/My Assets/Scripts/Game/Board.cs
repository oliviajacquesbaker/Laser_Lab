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
    private GameObject[] Corners;

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
    public Vector2Int Size { get { return new Vector2Int(Width, Height); } }

    public Board(int width, int height)
    {
        m_height = height;
        m_width = width;
        Tiles = new BoardObject[width * height];
        Walls = new WallObject[Width * 2 + Height * 2];
        Corners = new GameObject[4];
    }

    public Board() : this(6, 4) { }

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

    public void OnLaserPathCalculated()
    {
        for (int i = 0; i < Walls.Length; i++)
        {
            if (Walls[i])
                Walls[i].OnLaserPathCalculated();
        }

        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i])
                Tiles[i].OnLaserPathCalculated();
        }
    }

    public int ConvertPositionToWallCoord(Vector2Int pos)
    {
        return ConvertPositionToWallCoord(pos, Size);
    }

    public int ConvertPositionToWallCoord(Vector2Int pos, Vector2Int size)
    {
        if (pos.x < 0)
        {
            return pos.y;
        }
        else if (pos.y >= size.y)
        {
            return size.y + pos.x;
        }
        else if (pos.x >= size.x)
        {
            return size.y * 2 + size.x - pos.y - 1;
        }
        else if (pos.y < 0)
        {
            return size.y * 2 + size.x * 2 - pos.x - 1;
        }
        return -1;
    }

    public int ConvertPositionToBoardCoord(Vector2Int pos)
    {
        return ConvertPositionToBoardCoord(pos, Size);
    }

    public int ConvertPositionToBoardCoord(Vector2Int pos, Vector2Int size)
    {
        return pos.x + pos.y * size.x;
    }

    public int ConvertPositionToCornerCoord(Vector2Int pos)
    {
        return ConvertPositionToCornerCoord(pos, Size);
    }

    public int ConvertPositionToCornerCoord(Vector2Int pos, Vector2Int size)
    {
        if (pos.x == -1 && pos.y == -1)
            return 0;
        if (pos.x == size.x && pos.y == -1)
            return 3;
        if (pos.x == -1 && pos.y == size.y)
            return 1;
        if (pos.x == size.x && pos.y == size.y)
            return 2;

        return -1;
    }

    public BoardObject GetBoardObject(int x, int y)
    {
        return GetBoardObject(new Vector2Int(x, y));
    }

    public BoardObject GetBoardObject(Vector2Int pos)
    {
        if (IsWithinBoard(pos))
            return Tiles[ConvertPositionToBoardCoord(pos)];
        else throw new IndexOutOfRangeException("Position " + pos + " is outside the bounds of the board");
    }

    public WallObject GetWallObject(int x, int y)
    {
        return GetWallObject(new Vector2Int(x, y));
    }

    public WallObject GetWallObject(Vector2Int pos)
    {
        if (IsWallTile(pos))
            return Walls[ConvertPositionToWallCoord(pos)];
        else throw new IndexOutOfRangeException("Position " + pos + " is not within the bounds of the walls");
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
        else throw new IndexOutOfRangeException("Position " + pos + " is outside the bounds of the walls");
    }

    public GameObject GetCornerObject(int x, int y)
    {
        return GetCornerObject(new Vector2Int(x, y));
    }

    public GameObject GetCornerObject(Vector2Int pos)
    {
        if (IsCornerTile(pos))
            return Corners[ConvertPositionToCornerCoord(pos)];
        else throw new IndexOutOfRangeException("Position " + pos + " is not a corner tile");
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

    public bool IsCornerTile(Vector2Int pos)
    {
        return ConvertPositionToCornerCoord(pos) != -1;
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

    private Direction GetCornerOrientation(Vector2Int pos)
    {
        if (pos.x == -1)
        {
            if (pos.y == -1)
                return Direction.UP;
            else
                return Direction.RIGHT;
        }
        else
        {
            if (pos.y == -1)
                return Direction.LEFT;
            else
                return Direction.DOWN;
        }
    }

    public Vector2Int FindObject(LaserLabObject obj)
    {
        for (int i = -1; i <= Width; i++)
        {
            for (int j = -1; j <= Height; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                if (obj == GetLaserLabObject(pos))
                {
                    return pos;
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    public ILaserEmitter[] FindEmitters()
    {
        List<ILaserEmitter> emitters = new List<ILaserEmitter>();

        for (int i = 0; i < Walls.Length; i++)
        {
            if (Walls[i] is ILaserEmitter)
                emitters.Add(Walls[i] as ILaserEmitter);
        }

        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i] is ILaserEmitter)
                emitters.Add(Tiles[i] as ILaserEmitter);
        }

        return emitters.ToArray();
    }

    public ILaserReceiver[] FindReceivers()
    {
        List<ILaserReceiver> emitters = new List<ILaserReceiver>();

        for (int i = 0; i < Walls.Length; i++)
        {
            if (Walls[i] is ILaserReceiver)
                emitters.Add(Walls[i] as ILaserReceiver);
        }

        for (int i = 0; i < Tiles.Length; i++)
        {
            if (Tiles[i] is ILaserReceiver)
                emitters.Add(Tiles[i] as ILaserReceiver);
        }

        return emitters.ToArray();
    }

#if UNITY_EDITOR

    public void ReloadTiles()
    {
        for (int i = -1; i <= Width; i++)
        {
            for (int j = -1; j <= Height; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                if (IsCornerTile(pos))
                {
                    int index = ConvertPositionToCornerCoord(pos);
                    if (Corners[index] != null)
                        DestroyImmediate(Corners[index]);
                    Corners[index] = CreateCornerAt(pos);
                } else if (IsWallTile(pos))
                {
                    int index = ConvertPositionToWallCoord(pos);
                    WallObject oldWall = Walls[index];
                    if (Walls[index] != null)
                    {
                        Walls[index] = CreateWallAtFrom(pos, oldWall);
                        DestroyImmediate(oldWall.gameObject);
                    }
                    else
                    {
                        Walls[index] = CreateWallAt(pos);
                    }
                } else if (IsWithinBoard(pos))
                {
                    int index = ConvertPositionToBoardCoord(pos);
                    BoardObject oldObj = Tiles[index];
                    if (oldObj != null)
                    {
                        Tiles[index] = CreateBoardObjectAtFrom(pos, Tiles[index]);
                        DestroyImmediate(oldObj.gameObject);
                    }
                }
            }
        }

        RemoveExtraObjects();
    }

    public void RemoveExtraObjects()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;

            bool found = false;

            foreach (GameObject go in Corners)
            {
                if (go == obj)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                foreach (WallObject wo in Walls)
                {
                    if (wo && wo.gameObject == obj)
                    {
                        found = true;
                        break;
                    }
                }

            if (!found)
                foreach (BoardObject bo in Tiles)
                {
                    if (bo && bo.gameObject == obj)
                    {
                        found = true;
                        break;
                    }
                }

            if (!found)
            {
                DestroyImmediate(obj);
                i--;
            }
        }
    }

    public void Resize(Vector2Int newSize)
    {
        BoardObject[] newTiles = new BoardObject[newSize.x * newSize.y];
        WallObject[] newWalls = new WallObject[newSize.x * 2 + newSize.y * 2];

        Vector2Int oldSize = Size;
        m_width = newSize.x;
        m_height = newSize.y;

        //assign top and bottom walls
        for (int i = 0; i < newSize.x || i < oldSize.x; i++)
        {
            //define wall locations
            Vector2Int bottom = new Vector2Int(i, -1);
            Vector2Int oldTop = new Vector2Int(i, oldSize.y);
            Vector2Int newTop = new Vector2Int(i, newSize.y);

            //find top indeces
            int oldTopIndex = ConvertPositionToWallCoord(oldTop, oldSize);
            int newTopIndex = ConvertPositionToWallCoord(newTop, newSize);

            //find bottom indeces
            int oldBottomIndex = ConvertPositionToWallCoord(bottom, oldSize);
            int newBottomIndex = ConvertPositionToWallCoord(bottom, newSize);

            //copy existing walls
            if (i < newSize.x && i < oldSize.x)
            {
                if (!Walls[oldBottomIndex])
                    newWalls[newBottomIndex] = CreateWallAt(bottom);
                else
                    newWalls[newBottomIndex] = Walls[oldBottomIndex];

                if (!Walls[oldTopIndex])
                    newWalls[newTopIndex] = CreateWallAt(newTop);
                else
                {
                    newWalls[newTopIndex] = Walls[oldTopIndex];
                    newWalls[newTopIndex].gameObject.transform.position = new Vector3(newTop.x, 0, newTop.y);
                }
            }
            //add new walls
            else if (i >= oldSize.x)
            {
                newWalls[newBottomIndex] = CreateWallAt(bottom);
                newWalls[newTopIndex] = CreateWallAt(newTop);
            }
            //remove extra walls
            else if (i >= newSize.x)
            {
                DestroyImmediate(Walls[oldBottomIndex].gameObject);
                DestroyImmediate(Walls[oldTopIndex].gameObject);
            }
        }

        //assign left and right walls
        for (int j = 0; j < newSize.y || j < oldSize.y; j++)
        {
            //define wall locations
            Vector2Int left = new Vector2Int(-1, j);
            Vector2Int oldRight = new Vector2Int(oldSize.x, j);
            Vector2Int newRight = new Vector2Int(newSize.x, j);

            //find right indeces
            int oldRightIndex = ConvertPositionToWallCoord(oldRight, oldSize);
            int newRightIndex = ConvertPositionToWallCoord(newRight, newSize);

            //find left indeces
            int oldLeftIndex = ConvertPositionToWallCoord(left, oldSize);
            int newLeftIndex = ConvertPositionToWallCoord(left, newSize);

            //copy existing walls
            if (j < newSize.y && j < oldSize.y)
            {
                if (!Walls[oldLeftIndex])
                    newWalls[newLeftIndex] = CreateWallAt(left);
                else
                    newWalls[newLeftIndex] = Walls[oldLeftIndex];

                if (!Walls[oldRightIndex])
                    newWalls[newRightIndex] = CreateWallAt(newRight);
                else
                {
                    newWalls[newRightIndex] = Walls[oldRightIndex];
                    newWalls[newRightIndex].gameObject.transform.position = new Vector3(newRight.x, 0, newRight.y);
                }
            }
            //add new walls
            else if (j >= oldSize.y)
            {
                newWalls[newLeftIndex] = CreateWallAt(left);
                newWalls[newRightIndex] = CreateWallAt(newRight);
            }
            //remove extra walls
            else if (j >= newSize.y)
            {
                DestroyImmediate(Walls[oldLeftIndex].gameObject);
                DestroyImmediate(Walls[oldRightIndex].gameObject);
            }
        }

        for (int i = 0; i < newSize.x || i < oldSize.x; i++)
        {
            for (int j = 0; j < newSize.y || j < oldSize.y; j++)
            {
                //convert position to indeces
                Vector2Int pos = new Vector2Int(i, j);
                int oldIndex = ConvertPositionToBoardCoord(pos, oldSize);
                int newIndex = ConvertPositionToBoardCoord(pos, newSize);

                //Copy existing objects
                if (i < newSize.x && j < newSize.y && i < oldSize.x && j < oldSize.y)
                {
                    newTiles[newIndex] = Tiles[oldIndex];
                }
                //Remove extra objects
                else if (i >= newSize.x || j >= newSize.y)
                {
                    if (Tiles[oldIndex])
                        DestroyImmediate(Tiles[oldIndex].gameObject);
                }
            }
        }

        //copy new values to this object
        Walls = newWalls;
        Tiles = newTiles;

        //reposition corners
        if (Corners[0] != null)
        {
            Corners[1].transform.position = new Vector3(-1, 0, Height);
            Corners[2].transform.position = new Vector3(Width, 0, Height);
            Corners[3].transform.position = new Vector3(Width, 0, -1);
        }
    }

    public WallObjectBlank CreateWallAt(Vector2Int pos)
    {
        return (WallObjectBlank)CreateWallAt(pos, typeof(WallObjectBlank));
    }

    public WallObject CreateWallAt(Vector2Int pos, Type type)
    {
        //find model
        TileSet.Piece piece;
        if (!type.IsSubclassOf(typeof(WallObject)))
            return null;

        if (tileSet != null)
            piece = tileSet.FindPieceFromType(type);
        else return null;

        if (piece == null)
        {
            Debug.LogError("Tileset does not contain type \"" + type.ToString() + '"');
            return null;
        }

        //instantiate model
        GameObject newWallObject = Instantiate(piece.Tile);

        //configure gameobject
        newWallObject.name = "Wall (" + pos.x + "," + pos.y + ")";
        newWallObject.transform.SetParent(transform);

        //configure wallobject
        WallObject wallObject = newWallObject.GetComponent<WallObject>();

        if (!wallObject)
        {
            DestroyImmediate(newWallObject);
            Debug.LogError("Wall object prefab does not contain a wall object script");
            return null;
        }

        if (piece.Tile == null)
        {
            Debug.LogError("Tileset does not contain type \"" + type.ToString() + '"');
            return null;
        }

        //orient gameObject
        Direction wallDirection = GetWallOrientation(pos);
        newWallObject.transform.position = new Vector3(pos.x, 0, pos.y);
        newWallObject.transform.rotation = Quaternion.Euler(0, (-90 * (int)piece.modelOrientation) + 90 * (int)wallDirection, 0);

        return wallObject;
    }

    public T CreateWallAtFrom<T>(Vector2Int pos, T oldWallObject) where T : WallObject
    {
        //find model
        TileSet.Piece piece;
        if (tileSet != null)
            piece = tileSet.FindPieceFromType(oldWallObject.GetType());
        else return null;

        if (piece == null)
        {
            Debug.LogError("Tileset does not contain type \"" + typeof(T).ToString() + '"');
            return null;
        }

        if (piece.Tile == null)
        {
            Debug.LogError("Tileset does not contain type \"" + typeof(T).ToString() + '"');
            return null;
        }

        //instantiate model
        GameObject newWallObject = Instantiate(piece.Tile);

        //configure gameobject
        newWallObject.name = "Wall (" + pos.x + "," + pos.y + ")";
        newWallObject.transform.SetParent(transform);

        //copy wallObject
        T wallObject = CopyComponent(oldWallObject, newWallObject);

        //orient gameObject
        Direction wallDirection = GetWallOrientation(pos);
        newWallObject.transform.position = new Vector3(pos.x, 0, pos.y);
        newWallObject.transform.rotation = Quaternion.Euler(0, (-90 * (int)piece.modelOrientation) + 90 * (int)wallDirection, 0);

        return wallObject;
    }

    public BoardObject CreateBoardObjectAt(Vector2Int pos, Type type)
    {
        //find model
        if (!type.IsSubclassOf(typeof(BoardObject)))
            return null;
        TileSet.Piece piece;
        if (tileSet != null)
            piece = tileSet.FindPieceFromType(type);
        else return null;

        if (piece == null)
        {
            Debug.LogError("Tileset does not contain type \"" + type.ToString() + '"');
            return null;
        }

        if (piece.Tile == null)
        {
            Debug.LogError("Tileset does not contain type \"" + type.ToString() + '"');
            return null;
        }

        //instantiate model
        GameObject newBoardObject = Instantiate(piece.Tile);

        //configure gameobject
        newBoardObject.name = "Board Object (" + pos.x + "," + pos.y + ")";
        newBoardObject.transform.SetParent(transform);

        //configure boardobject
        BoardObject boardObject = newBoardObject.GetComponent<BoardObject>();

        if (!boardObject)
        {
            DestroyImmediate(newBoardObject);
            Debug.LogError("Board object prefab does not contain a board object script");
            return null;
        }

        //orient gameObject
        newBoardObject.transform.position = new Vector3(pos.x, 0, pos.y);
        newBoardObject.transform.rotation = Quaternion.Euler(0, (-90 * (int)piece.modelOrientation) + 90 * (int)boardObject.Orientation, 0);

        boardObject.Refresh();

        return boardObject;
    }

    public T CreateBoardObjectAtFrom<T>(Vector2Int pos, T oldWallObject) where T : BoardObject
    {
        //find model
        TileSet.Piece piece;
        if (tileSet != null)
            piece = tileSet.FindPieceFromType(oldWallObject.GetType());
        else return null;

        if (piece == null)
        {
            Debug.LogError("Tileset does not contain type \"" + typeof(T).ToString() + '"');
            return null;
        }

        if (piece.Tile == null)
        {
            Debug.LogError("Tileset does not contain type \"" + typeof(T).ToString() + '"');
            return null;
        }

        //instantiate model
        GameObject newBoardObject = Instantiate(piece.Tile);

        //configure gameobject
        newBoardObject.name = "Board Object (" + pos.x + "," + pos.y + ")";
        newBoardObject.transform.SetParent(transform);

        //configure boardobject
        T boardObject = CopyComponent(oldWallObject, newBoardObject);

        //orient gameObject
        newBoardObject.transform.position = new Vector3(pos.x, 0, pos.y);
        newBoardObject.transform.rotation = Quaternion.Euler(0, (-90 * (int)piece.modelOrientation) + 90 * (int)boardObject.Orientation, 0);

        boardObject.Refresh();

        return boardObject;
    }

    public GameObject CreateCornerAt(Vector2Int pos)
    {
        //find model
        TileSet.EnvironmentPiece piece;
        if (tileSet != null)
            piece = tileSet.environment.Corner;
        else return null;

        if (piece == null)
        {
            Debug.LogError("Tileset does not contain a corner object");
            return null;
        }

        if (piece.Tile == null)
        {
            Debug.LogError("Tileset does not contain a corner object");
            return null;
        }

        //instantiate model
        GameObject newWallObject = Instantiate(piece.Tile);

        //configure gameobject
        newWallObject.name = "Corner (" + pos.x + "," + pos.y + ")";
        newWallObject.transform.SetParent(transform);

        //orient gameObject
        newWallObject.transform.position = new Vector3(pos.x, 0, pos.y);
        newWallObject.transform.rotation = Quaternion.Euler(0, (-90 * (int)piece.modelOrientation) + 90 * (int)GetCornerOrientation(pos), 0);

        return newWallObject;
    }

    T CopyComponent<T>(T original, GameObject destination) where T : Component
    {
        Type type = original.GetType();
        Component copy = destination.GetComponent<T>();
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            if(Attribute.GetCustomAttribute(field, typeof(PrefabDataAttribute)) == null)
                field.SetValue(copy, field.GetValue(original));
        }
        return copy as T;
    }
#endif
}
