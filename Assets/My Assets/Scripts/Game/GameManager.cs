using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    Level level;
    LaserLabObject previousHover = null;
    public GameObject visualLaserPrefab;
    public CameraController cameraController;
    public int selectedObjectIndex = -1;
    public List<BoardObject> UnplacedObjects;
    public ObjectListDisplayer displayer;
    public BoardObject SelectedObject { get { return UnplacedObjects[selectedObjectIndex]; } }
    private List<VisualLaser> visualLasers;

    void Start()
    {
        visualLasers = new List<VisualLaser>();
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
        CalculateLaserPaths();
        PositionCamera();
    }

    private void PositionCamera()
    {
        if (cameraController)
        {
            Vector3 center = new Vector3(level.size.x - 1, 1, level.size.y - 1);
            cameraController.PositionCamera(center / 2, level.size);
        }
    }

    void Update()
    {
        if (Pause.Current && !Pause.Current.paused)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
                MouseEvents();
            else if (selectedObjectIndex < UnplacedObjects.Count && selectedObjectIndex > -1)
                UnplacedObjects[selectedObjectIndex].SetPreview(false);
        }
    }

    private void AddToUnplaced(BoardObject obj)
    {
        SelectObject(UnplacedObjects.Count);
        UnplacedObjects.Add(obj);
        displayer.ReloadButtons();
        CalculateLaserPaths();
    }

    private void Place(Vector2Int pos)
    {
        BoardObject obj = UnplacedObjects[selectedObjectIndex];
        UnplacedObjects.RemoveAt(selectedObjectIndex);
        SelectObject(-1);

        obj.Move(pos);
        obj.Place();
        level.board.SetBoardObject(pos, obj);
        displayer.ReloadButtons();
        CalculateLaserPaths();
    }

    private void CalculateLaserPaths()
    {
        ILaserEmitter[] emitters = level.board.FindEmitters();
        ILaserReceiver[] receivers = level.board.FindReceivers();

        //reset receiver status
        for (int i = 0; i < receivers.Length; i++)
        {
            receivers[i].ResetLaserCondition();
        }

        //generate initial lasers from emitters
        List<Laser> startingLasers = new List<Laser>();
        for (int i = 0; i < emitters.Length; i++)
        {
            Laser[] newLasers = emitters[i].OnLaserEmit();
            if (emitters[i] is WallObject)
            {
                newLasers[0].direction = (emitters[i] as WallObject).GetDirection(level.board);
            }

            for (int j = 0; j < newLasers.Length; j++)
            {
                newLasers[j].origin = emitters[i].GetPosition();
            }

            startingLasers.AddRange(newLasers);
        }

        //create a list to store all the lasers
        List<Laser> allLasers = new List<Laser>();

        //recursivly solve all laser paths
        for (int i = 0; i < startingLasers.Count; i++)
        {
            RecursiveLaserPath(ref allLasers, startingLasers[i], 0, 128);
        }

        //check for a win
        bool success = true;
        for (int i = 0; i < receivers.Length && success; i++)
        {
            if (!receivers[i].IsLaserConditionSatisfied())
                success = false;
        }

        if (success)
            Debug.Log("Win");

        for (int i = 0; i < visualLasers.Count; i++)
        {
            Destroy(visualLasers[i].gameObject);
        }

        visualLasers = new List<VisualLaser>();

        for (int i = 0; i < allLasers.Count; i++)
        {
            GameObject laserObject = Instantiate(visualLaserPrefab, transform);
            VisualLaser visualLaser = laserObject.GetComponent<VisualLaser>();
            visualLaser.SetProperties(allLasers[i]);
            visualLasers.Add(visualLaser);
        }
    }

    private void RecursiveLaserPath(ref List<Laser> lasers, Laser current, int index, int max)
    {
        lasers.Add(current);

        if (index >= max)
            return;

        Vector2Int origin = current.origin;

        int distance = 1;
        Vector2Int direction = current.GetDirectionVector();

        while (true)
        {
            LaserLabObject nextObject = level.board.GetLaserLabObject(origin + (direction * distance));
            if (nextObject && nextObject is ILaserTarget)
            {
                current.length = distance;
                ILaserTarget nextTarget = nextObject as ILaserTarget;
                Laser[] nextLasers = nextTarget.OnLaserHit(current);

                for (int i = 0; i < nextLasers.Length; i++)
                {
                    nextLasers[i].origin = nextTarget.GetPosition();
                    RecursiveLaserPath(ref lasers, nextLasers[i], index + 1, max);
                }

                return;
            }

            distance++;
        }
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

                        SelectObject(-1);
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
                            SelectObject(-1);
                    }

                    if (selectedObjectIndex > -1)
                    {
                        SelectedObject.SetPreview(false);
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
                            SelectObject(-1);
                    } else if (Input.GetMouseButtonDown(1))
                        SelectObject(-1);

                    if (selectedObjectIndex > -1)
                    {
                        if (level.board.GetBoardObject(pos))
                            SelectedObject.SetPreview(false);
                        else
                        {
                            SelectedObject.SetPreview(true);
                            SelectedObject.Move(pos);
                        }
                    }
                } else if (target is WallObject)
                {
                    WallObject wall = target as WallObject;
                    Vector2Int pos = wall.getPos();

                    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                        SelectObject(-1);

                    if (selectedObjectIndex > -1)
                    {
                        SelectedObject.SetPreview(false);
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
                    SelectObject(-1);

                if (selectedObjectIndex > -1)
                {
                    SelectedObject.SetPreview(false);
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

            if (selectedObjectIndex > -1)
            {
                SelectedObject.SetPreview(false);
            }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                SelectObject(-1);
        }
    }

    public void SelectObject(int index)
    {
        if (selectedObjectIndex < UnplacedObjects.Count && selectedObjectIndex > -1)
        {
            UnplacedObjects[selectedObjectIndex].SetPreview(false);
        }

        selectedObjectIndex = index;
    }
}
