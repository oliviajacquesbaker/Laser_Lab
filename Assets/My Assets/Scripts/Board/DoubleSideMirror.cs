using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSideMirror : BoardObject
{
    DoubleSideMirror()
    {
        Rotation = 0;
    }

    DoubleSideMirror(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Rotation = rotation;
    }

    private Laser.Direction getNewDirection(Laser laser, int side)
    {
        Laser.Direction dir = laser.direction;
        if (side == 0)
        {
            if (Rotation == 0)
            {
                if (dir == Laser.Direction.DOWN)
                    return Laser.Direction.RIGHT;
                else if (dir == Laser.Direction.LEFT)
                    return Laser.Direction.UP;
            }
            else if (Rotation == 1)
            {
                if (dir == Laser.Direction.DOWN)
                    return Laser.Direction.LEFT;
                else if (dir == Laser.Direction.RIGHT)
                    return Laser.Direction.UP;
            }
            else if (Rotation == 2)
            {
                if (dir == Laser.Direction.UP)
                    return Laser.Direction.LEFT;
                else if (dir == Laser.Direction.RIGHT)
                    return Laser.Direction.DOWN;
            }
            else
            {
                if (dir == Laser.Direction.UP)
                    return Laser.Direction.RIGHT;
                else if (dir == Laser.Direction.LEFT)
                    return Laser.Direction.DOWN;
            }
        }
        else
        {
            if (Rotation == 0)
            {
                if (dir == Laser.Direction.UP)
                    return Laser.Direction.LEFT;
                else if (dir == Laser.Direction.RIGHT)
                    return Laser.Direction.DOWN;
            }
            else if (Rotation == 1)
            {
                if (dir == Laser.Direction.UP)
                    return Laser.Direction.RIGHT;
                else if (dir == Laser.Direction.LEFT)
                    return Laser.Direction.DOWN;
            }
            else if (Rotation == 2)
            {
                if (dir == Laser.Direction.DOWN)
                    return Laser.Direction.RIGHT;
                else if (dir == Laser.Direction.LEFT)
                    return Laser.Direction.UP;
            }
            else
            {
                if (dir == Laser.Direction.DOWN)
                    return Laser.Direction.LEFT;
                else if (dir == Laser.Direction.RIGHT)
                    return Laser.Direction.UP;
            }
        }

        return Laser.Direction.UP;
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        //to get side of mirror the laser hits

        Vector3 laserOrigin = new Vector3(laser.origin.x, laser.origin.y);
        Vector3 mirrorLaserDir = transform.position - laserOrigin;
        float dotProduct = Vector3.Dot(mirrorLaserDir, transform.forward);
        int side;

        if (dotProduct > 0)
            side = 0;
        else if (dotProduct < 0)
            side = 1;
        else
            return new Laser[0];

        Vector2Int newOrigin = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Laser.Direction newDirection = getNewDirection(laser, side);
        Laser newLaser = new Laser(newOrigin, newDirection, laser.red, laser.green, laser.blue);
        Laser[] returning = new Laser[1];
        returning[0] = newLaser;
        return returning;


    }
}
