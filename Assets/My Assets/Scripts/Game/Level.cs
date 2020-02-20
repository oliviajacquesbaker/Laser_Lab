using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Level : MonoBehaviour
{
    [HideInInspector]
    public BoardObject[,] board;
    [HideInInspector]
    public List<BoardObject> unplaced;
    [HideInInspector]
    public WallObject[] walls;
    [HideInInspector]
    public Vector2Int minPos;
    [HideInInspector]
    public Vector2Int maxPos;

    public int width { get { return maxPos.x - minPos.x + 1; } }
    public int height { get { return maxPos.y - minPos.y + 1; } }

    void Update()
    {
#if UNITY_EDITOR
        if (!UnityEditor.EditorApplication.isPlaying)
        {
            BoardObject[] boardObjectsFound = FindObjectsOfType<BoardObject>();
            WallObject[] wallObjectsFound = FindObjectsOfType<WallObject>();

            minPos = new Vector2Int(int.MaxValue, int.MaxValue);
            maxPos = new Vector2Int(int.MinValue, int.MinValue);

            foreach (WallObject w in wallObjectsFound)
            {
                int x = Mathf.RoundToInt(w.transform.position.x);
                int y = Mathf.RoundToInt(w.transform.position.z);

                if (x - 1 > maxPos.x)
                    maxPos.x = x - 1;
                if (y - 1 > maxPos.y)
                    maxPos.y = y - 1;

                if (x + 1 < minPos.x)
                    minPos.x = x + 1;
                if (y + 1 < minPos.y)
                    minPos.y = y + 1;
            }

            board = new BoardObject[width, height];
            walls = new WallObject[width * 2 + height * 2];
            unplaced = new List<BoardObject>();

            foreach (WallObject w in wallObjectsFound)
            {
                Vector2Int levelCoords = convertCoordinates(w.transform);

                if (levelCoords.x < minPos.x)
                {
                    walls[levelCoords.y] = w;
                }
                else if (levelCoords.y > maxPos.y)
                {
                    walls[levelCoords.x + height] = w;
                }
                else if (levelCoords.x > maxPos.x)
                {
                    walls[height * 2 + width - levelCoords.y - 1] = w;
                }
                else if (levelCoords.y < minPos.y)
                {
                    walls[height * 2 + width * 2 - levelCoords.x - 1] = w;
                }
                else
                {
                    throw new System.Exception("Invalid Wall Position");
                }
            }

            foreach (BoardObject b in boardObjectsFound)
            {
                if (b.placed)
                {
                    Vector2Int levelCoords = convertCoordinates(b.transform);
                    board[levelCoords.x, levelCoords.y] = b;
                } else
                {
                    unplaced.Add(b);
                }
            }
        }
#endif
    }

    public Vector2Int convertCoordinates (Transform transform)
    {
        int x = Mathf.RoundToInt(transform.position.x);
        int y = Mathf.RoundToInt(transform.position.z);
        x -= minPos.x;
        y -= minPos.y;
        return new Vector2Int(x, y);
    }
}