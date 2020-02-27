using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectTwoWayMirror : BoardObject
{
    public BoardObjectTwoWayMirror()
    {
        Orientation = 0;
    }
    
    public BoardObjectTwoWayMirror(int rotation)
    {
        Orientation = (Direction)(rotation % 4);
    }

    private Direction getNewDirection(Laser laser, int face)
    {
        Direction dir = laser.direction;
        if (face == 0 || face == 1)
        {
            return Reflect(laser.direction);
        }
        else
        {
            Direction flippedOrientation = (Direction)(((int)Orientation + 2) % 4);
            return Reflect(laser.direction, flippedOrientation);
        }
    }

    public override Laser[] OnLaserHit(Laser laser)
    {

        int face = getFace(laser.direction, Orientation);
        Direction newDirection = getNewDirection(laser, face);
        Laser reflected = new Laser(newDirection, (laser.red/2), (laser.green/2), (laser.blue/2));
        Laser straightThrough = new Laser(laser.direction, (laser.red/2), (laser.green/2), (laser.blue/2));

        Laser[] returning = new Laser[2];
        returning[0] = reflected;
        returning[1] = straightThrough;
        return returning;

    }
    
}
