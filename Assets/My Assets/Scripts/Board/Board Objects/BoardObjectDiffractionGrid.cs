using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectDiffractionGrid : BoardObject
{
    public BoardObjectDiffractionGrid()
    {
        Orientation = 0;
    }

    
    public BoardObjectDiffractionGrid(int rotation)
    {
        Orientation = (Direction)(rotation % 4);
    }

    private Direction getNewDirection(Laser laser, int num)
    {

        if ((num == 0 && ((int)Orientation % 2 == 0)) || (num == 1 && ((int)Orientation % 2 == 1)))
        {
            Direction newdir = (Direction)(((int)laser.direction - 1) %4);
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

        Direction first25Dir = getNewDirection(laser, 0);
        Direction second25Dir = getNewDirection(laser, 1);
        Laser first25 = new Laser(first25Dir, (laser.red/4), (laser.green/4), (laser.blue/4));
        Laser mid50 = new Laser(laser.direction, (laser.red/2), (laser.green/2), (laser.blue/2));
        Laser second25 = new Laser(second25Dir, (laser.red/4), (laser.green/4), (laser.blue/4));

        Laser[] returning = new Laser[3];
        returning[0] = first25;
        returning[1] = mid50;
        returning[2] = second25;
        return returning;

    }
    
}
