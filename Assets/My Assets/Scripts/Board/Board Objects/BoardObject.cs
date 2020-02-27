using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BoardObject : LaserLabObject
{
    public Direction Orientation;
    public bool CanMove;
    public bool CanRotate;

    public BoardObject(Direction orientation, bool canMove, bool canRotate)
    {
        Orientation = orientation;
        CanMove = canMove;
        CanRotate = canRotate;
    }

    public BoardObject(Direction orientation) : this(orientation, true, true) { }

    public BoardObject() : this(Direction.UP, true, true) { }

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
