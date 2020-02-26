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
        if (rotation >= 0 && rotation <= 3)
            Orientation = rotation;
    }

    private Laser.Direction getNewDirection(Laser laser, int num)
    {
        Laser.Direction[] directions = new Laser.Direction[4];
        int index = 0;
        int original = 0;
        foreach (string type in System.Enum.GetNames(typeof(Laser.Direction)))
        {
            Laser.Direction dir = (Laser.Direction)System.Enum.Parse(typeof(Laser.Direction), type);
            if (dir == laser.direction)
                original = index;
            directions[index] = dir;
            index++;
        }

        if ((num == 0 && (Orientation == 0 || Orientation == 2)) || (num == 1 && (Orientation == 1 || Orientation == 3)))
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

        Laser.Direction first25Dir = getNewDirection(laser, 0);
        Laser.Direction second25Dir = getNewDirection(laser, 1);
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
