using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoWayMirror : BoardObject
{
    TwoWayMirror()
    {
        Rotation = 0;
    }

    TwoWayMirror(int rotation)
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
        Laser reflected = new Laser(newOrigin, newDirection, (laser.red/2), (laser.green/2), (laser.blue/2));
        Laser straightThrough = new Laser(newOrigin, laser.direction, (laser.red/2), (laser.green/2), (laser.blue/2));

        Laser[] returning = new Laser[2];
        returning[0] = reflected;
        returning[1] = straightThrough;
        return returning;

    }
}
