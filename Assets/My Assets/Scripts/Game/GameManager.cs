using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Level level;
    LaserLabObject previousHover = null;
    public int selectedObjectIndex = -1;
    //[HideInInspector]
    public List<BoardObject> UnplacedObjects;
    public ObjectListDisplayer displayer;

    void Start()
    {
        UnplacedObjects = new List<BoardObject>();
        level = FindObjectOfType<Level>();
        for (int i = 0; i < level.board.Width; i++)
        {
            for (int j = 0; j < level.board.Height; j++)
            {
                Vector2Int pos = new Vector2Int(i, j);
                BoardObject current = level.board.GetBoardObject(pos);
                if (current)
                {
                    if (!current.Placed)
                    {
                        UnplacedObjects.Add(current);
                        level.board.SetBoardObject(pos, null);
                    }
                }
            }
        }
        displayer.ReloadButtons();
    }

    void Update()
    {
        MouseEvents();
    }

    private void AddToUnplaced(BoardObject obj)
    {
        selectedObjectIndex = UnplacedObjects.Count;
        UnplacedObjects.Add(obj);
        displayer.ReloadButtons();
        CalculateLaserPaths();
    }

    private void Place(Vector2Int pos)
    {
        BoardObject obj = UnplacedObjects[selectedObjectIndex];
        UnplacedObjects.RemoveAt(selectedObjectIndex);
        selectedObjectIndex = -1;

        obj.Move(pos);
        obj.Place();
        level.board.SetBoardObject(pos, obj);
        displayer.ReloadButtons();
        CalculateLaserPaths();
    }

    private void CalculateLaserPaths()
    {

    }

    private void MouseEvents()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            LaserLabObject target = hit.collider.GetComponent<LaserLabObject>();

            if (target != null)
            {
                if (target != previousHover)
                {
                    if (previousHover != null)
                        previousHover.OnHoverExit();
                    target.OnHoverEnter();
                    previousHover = target;
                }

                if (target is BoardObject)
                {
                    BoardObject boardObject = target as BoardObject;
                    Vector2Int pos = boardObject.getPos();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (boardObject.CanRotate)
                        {
                            boardObject.Rotate();
                            CalculateLaserPaths();
                        }

                        selectedObjectIndex = -1;
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        if (boardObject.CanMove)
                        {
                            boardObject.Pickup();
                            level.board.SetBoardObject(pos, null);
                            AddToUnplaced(boardObject);
                        }
                        else
                        {
                            selectedObjectIndex = -1;
                        }
                    }
                } else if (target is FloorTile)
                {
                    FloorTile floor = target as FloorTile;
                    Vector2Int pos = floor.getPos();
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (!level.board.GetBoardObject(pos) && selectedObjectIndex != -1)
                        {
                            Place(pos);
                        }
                        else
                        {
                            selectedObjectIndex = -1;
                        }
                    } else if (Input.GetMouseButtonDown(1))
                    {
                        selectedObjectIndex = -1;
                    }
                } else if (target is WallObject)
                {
                    WallObject wall = target as WallObject;
                    Vector2Int pos = wall.getPos();
                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                    {
                        selectedObjectIndex = -1;
                    }
                }
            }
            else
            {
                if (previousHover != null)
                {
                    previousHover.OnHoverExit();
                    previousHover = null;
                }

                if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                    selectedObjectIndex = -1;
            }
        }
        else
        {
            if (previousHover != null)
            {
                previousHover.OnHoverExit();
                previousHover = null;
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                selectedObjectIndex = -1;
        }
    }
}
