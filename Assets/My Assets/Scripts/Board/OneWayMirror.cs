using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayMirror : BoardObject
{
    OneWayMirror()
    {
        Rotation = 0;
    }

    OneWayMirror(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Rotation = rotation;
    }

    private Laser.Direction getNewDirection(Laser laser)
    {
        Laser.Direction dir = laser.direction;
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

        return Laser.Direction.UP;
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        //assumes that the facing direction is the side that's reflective

        Vector3 laserOrigin = new Vector3(laser.origin.x, laser.origin.y);
        Vector3 mirrorLaserDir = transform.position - laserOrigin;
        float dotProduct = Vector3.Dot(mirrorLaserDir, Quaternion.Euler(0,45,0) * transform.forward);

        Vector2Int newOrigin = getBoardPosition();
        Laser newLaser;
        if (dotProduct > 0)
        {
            Laser.Direction newDirection = getNewDirection(laser);
            newLaser = new Laser(newOrigin, newDirection, laser.red, laser.green, laser.blue);
        }
        else
        {
            newLaser = new Laser(newOrigin, laser.direction, laser.red, laser.green, laser.blue);
        }

        Laser[] returning = new Laser[1];
        returning[0] = newLaser;
        return returning;

    }
}
