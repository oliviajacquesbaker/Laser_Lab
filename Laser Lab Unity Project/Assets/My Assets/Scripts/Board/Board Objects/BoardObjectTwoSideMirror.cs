using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectTwoSideMirror : BoardObject
{
    public BoardObjectTwoSideMirror()
    {
        Orientation = 0;
    }
    
    public BoardObjectTwoSideMirror(int rotation)
    {
        Orientation = (Direction)(rotation % 4);
    }

    private Direction getNewDirection(Laser laser, int face)
    {
        return Reflect(laser.direction);
        
    }

    public override Laser[] OnLaserHit(Laser laser)
    { 

        int face = getFace(laser.direction, Orientation);
        Direction newDirection = getNewDirection(laser, face);
        Laser newLaser = new Laser(newDirection, laser.red, laser.green, laser.blue);
        Laser[] returning = new Laser[1];
        returning[0] = newLaser;
        return returning;


    }
    
}