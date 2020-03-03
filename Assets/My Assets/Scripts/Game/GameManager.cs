using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Level level;
    LaserLabObject previousHover = null;

    void Start()
    {
        level = FindObjectOfType<Level>();
    }

    void Update()
    {
        MouseEvents();
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
                    BoardObject boardObject = (target as BoardObject);
                    if (Input.GetMouseButtonDown(0))
                    {
                        boardObject.Rotate();
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        Debug.LogWarning("Object removal not implemented");
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
            }
        }
        else
        {
            if (previousHover != null)
            {
                previousHover.OnHoverExit();
                previousHover = null;
            }
        }
    }
}
