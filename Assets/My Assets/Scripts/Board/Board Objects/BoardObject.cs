using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable][RequireComponent(typeof(Renderer))]
public abstract class BoardObject : LaserLabObject, ILaserTarget, IRefreshable
{
    public Direction Orientation = Direction.UP;
    public bool CanMove = true;
    public bool CanRotate = true;
    public bool Placed = false;
    public bool RandomizeOrientation = true;

    private new Renderer renderer;

    public abstract Laser[] OnLaserHit(Laser laser);

    private void Start()
    {
        renderer = GetComponent<Renderer>();

        if (RandomizeOrientation)
            Orientation = (Direction)(Random.value * 4);

        Refresh();
    }

    public void Refresh()
    {
        transform.rotation = Quaternion.Euler(0, (int)Orientation * 90, 0);

        if (Application.isPlaying)
        {
            renderer.material.SetColor("_BaseColor", (CanMove || CanRotate) ? Color.white : Color.grey);
            gameObject.SetActive(Placed);
        }
    }

    public void Rotate()
    {
        Orientation = (Direction)(((int)Orientation + 1) % 4);
        transform.rotation = Quaternion.Euler(0, (int)Orientation * 90, 0);
    }

    public void RotateTo(Direction newOrientation)
    {
        Orientation = newOrientation;
        transform.rotation = Quaternion.Euler(0, (int)Orientation * 90, 0);
    }

    public void Move(Vector2Int pos)
    {
        transform.position = new Vector3(pos.x, 0, pos.y);
    }

    public void Place()
    {
        Placed = true;
        Refresh();
    }

    public void Pickup()
    {
        Placed = false;
        Refresh();
    }

    //Returns the new direction a beam would take if reflected off a mirror at angle '\' when rotation = 0,2 or '/' when rotation = 1,3
    public static Direction Reflect(Direction dir, Direction orientation)
    {
        switch ((int)orientation % 2)
        {
            case 0:
                switch ((int)dir / 2)
                {
                    case 0:
                        return dir + 3 % 4;
                    case 1:
                        return dir + 1 % 4;
                    default:
                        return dir;
                }
            case 1:
                switch ((int)dir / 2)
                {
                    case 0:
                        return dir + 1 % 4;
                    case 1:
                        return dir + 3 % 4;
                    default:
                        return dir;
                }
            default:
                return dir;
        }
    }
    
    public Direction Reflect(Direction dir)
    {
        return Reflect(dir, Orientation);
    }

    /* Returns a direction to represent the relative face on the board
     * object that is hit by a laser moving in the direction dir.
     * 
     * If the object is rotated to the "right" (1) and a laser is going "left" (3), the result should
     * be "up" (0) because the face in contact would be the top relative to the original rotation.
     */
    public static int getFace(Direction dir, Direction orientation)
    {
        return (((int)dir + 6) % 4) - (int)orientation;
    }

    public int getFace(Direction dir)
    {
        return getFace(dir, Orientation);
    }
}
