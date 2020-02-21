using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prism : BoardObject
{
    Prism()
    {
        Rotation = 0;
    }

    Prism(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Rotation = rotation;
    }

    private Laser.Direction getNewDirection(Laser laser, int color)
    {
        Laser.Direction[] directions = new Laser.Direction[4];
        int index = 0;
        int original = 0;
        foreach(string type in System.Enum.GetNames(typeof(Laser.Direction)))
        {
            Laser.Direction dir = (Laser.Direction)System.Enum.Parse(typeof(Laser.Direction), type);
            if (dir == laser.direction)
                original = index;
            directions[index] = dir;
            index++;
        }

        if((color == 0 && (Rotation == 0 || Rotation == 2)) || (color == 1 && (Rotation == 1 || Rotation == 3)))
        {
            if (index == 0)
                index = 3;
            else
                index--;
            return directions[index];
        }
        else
        {
            if (index == 3)
                index = 0;
            else
                index++;
            return directions[index];
        }

    }

    public override Laser[] OnLaserHit(Laser laser)
    {

        Vector2Int newOrigin = getBoardPosition();
        Laser.Direction redDirection = getNewDirection(laser, 0);
        Laser.Direction blueDirection = getNewDirection(laser, 1);
        Laser red = new Laser(newOrigin, redDirection, laser.red, 0, 0);
        Laser green = new Laser(newOrigin, laser.direction, 0, laser.green, 0);
        Laser blue = new Laser(newOrigin, blueDirection, 0, 0, laser.blue);

        Laser[] returning = new Laser[3];
        returning[0] = red;
        returning[1] = green;
        returning[2] = blue;
        return returning;

    }
}
