using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectOneWayMirror : BoardObject
{
    public BoardObjectOneWayMirror()
    {
        Orientation = 0;
    }

    
    public BoardObjectOneWayMirror(int rotation)
    {
        Orientation = (Direction)(rotation % 4);
    }

    private Direction getNewDirection(Laser laser)
    {
        return Reflect(laser.direction);
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        //assumes that the facing direction is the side that's reflective

        int face = getFace(laser.direction, Orientation);
        Laser newLaser;
        if (face == 0)
        {
            Direction newDirection = getNewDirection(laser);
            newLaser = new Laser(newDirection, laser.red, laser.green, laser.blue);
        }
        else
        {
            newLaser = new Laser(laser.direction, laser.red, laser.green, laser.blue);
        }

        Laser[] returning = new Laser[1];
        returning[0] = newLaser;
        return returning;

    }
    
}
