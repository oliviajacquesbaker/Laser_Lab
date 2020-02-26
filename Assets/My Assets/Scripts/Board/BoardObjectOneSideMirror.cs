using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectOneSideMirror : BoardObject
{
    //rotation of zero is a mirror in this position: \
    // with the facing direction (the mirrored side) facing northeast

    public BoardObjectOneSideMirror()
    {
        Orientation = 0;
    }

    public BoardObjectOneSideMirror(int rotation)
    {
        if(rotation>=0 && rotation <=3)
            Orientation = rotation;
    }

    private Laser.Direction getNewDirection(Laser laser)
    {
        Laser.Direction dir = laser.direction;
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

        return Laser.Direction.UP;
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        //assumes that the facing direction is the side with the mirror

        Vector3 laserOrigin = new Vector3(laser.origin.x, laser.origin.y);
        Vector3 mirrorLaserDir = transform.position - laserOrigin;
        float dotProduct = Vector3.Dot(mirrorLaserDir, transform.forward);

        if (dotProduct > 0)
        {
            Laser.Direction newDirection = getNewDirection(laser);
            Laser newLaser = new Laser(newDirection, laser.red, laser.green, laser.blue);
            Laser[] returning = new Laser[1];
            returning[0] = newLaser;
            return returning;
        }
        else
        {
            return new Laser[0];
        }

    }
}

