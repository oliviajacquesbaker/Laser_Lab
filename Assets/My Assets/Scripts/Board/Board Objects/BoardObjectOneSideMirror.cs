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
        Orientation = (Direction)(rotation % 4);
    }

    private Direction getNewDirection(Laser laser)
    {
        return Reflect(laser.direction);
    }

    public override Laser[] OnLaserHit(Laser laser)
    {
        int face = getFace(laser.direction, Orientation);

        if (face == 0)
        {
            Direction newDirection = getNewDirection(laser);
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

