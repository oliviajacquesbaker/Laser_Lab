using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectTwoWayMirror : BoardObject
{
    public BoardObjectTwoWayMirror()
    {
        Orientation = 0;
    }
    /*
    public BoardObjectTwoWayMirror(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Orientation = rotation;
    }

    private Laser.Direction getNewDirection(Laser laser, int side)
    {
        Laser.Direction dir = laser.direction;
        if (side == 0)
        {
            if (Orientation == 0)
            {
                if (dir == Laser.Direction.DOWN)
                    return Laser.Direction.RIGHT;
                else if (dir == Laser.Direction.LEFT)
                    return Laser.Direction.UP;
            }
            else if (Orientation == 1)
            {
                if (dir == Laser.Direction.DOWN)
                    return Laser.Direction.LEFT;
                else if (dir == Laser.Direction.RIGHT)
                    return Laser.Direction.UP;
            }
            else if (Orientation == 2)
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
            if (Orientation == 0)
            {
                if (dir == Laser.Direction.UP)
                    return Laser.Direction.LEFT;
                else if (dir == Laser.Direction.RIGHT)
                    return Laser.Direction.DOWN;
            }
            else if (Orientation == 1)
            {
                if (dir == Laser.Direction.UP)
                    return Laser.Direction.RIGHT;
                else if (dir == Laser.Direction.LEFT)
                    return Laser.Direction.DOWN;
            }
            else if (Orientation == 2)
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

        Laser.Direction newDirection = getNewDirection(laser, side);
        Laser reflected = new Laser(newDirection, (laser.red/2), (laser.green/2), (laser.blue/2));
        Laser straightThrough = new Laser(laser.direction, (laser.red/2), (laser.green/2), (laser.blue/2));

        Laser[] returning = new Laser[2];
        returning[0] = reflected;
        returning[1] = straightThrough;
        return returning;

    }
    */

    public override Laser[] OnLaserHit(Laser laser)
    {
        return null;
    }
}
