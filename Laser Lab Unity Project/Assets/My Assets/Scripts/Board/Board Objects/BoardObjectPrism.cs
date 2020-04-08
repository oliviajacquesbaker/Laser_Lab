using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectPrism : BoardObject
{
    public BoardObjectPrism()
    {
        Orientation = 0;
    }
    
    public BoardObjectPrism(int rotation)
    {
        Orientation = (Direction)(rotation % 4);
    }

    private Direction getNewDirection(Laser laser, int color)
    {
        if ((color == 0 && ((int)Orientation % 2 == 0)) || (color == 1 && ((int)Orientation % 2 == 1)))
        {
            Direction newdir = (Direction)(((int)laser.direction + 3) % 4);
            return newdir;
        }
        else
        {
            Direction newdir = (Direction)(((int)laser.direction + 1) % 4);
            return newdir;
        }
    }

    public override Laser[] OnLaserHit(Laser laser)
    {

        Direction redDirection = getNewDirection(laser, 0);
        Direction blueDirection = getNewDirection(laser, 1);
        Laser red = new Laser(redDirection, laser.red, 0, 0);
        Laser green = new Laser(laser.direction, 0, laser.green, 0);
        Laser blue = new Laser(blueDirection, 0, 0, laser.blue);

        Laser[] returning = new Laser[3];
        returning[0] = red;
        returning[1] = green;
        returning[2] = blue;
        return returning;

    }
    
}
