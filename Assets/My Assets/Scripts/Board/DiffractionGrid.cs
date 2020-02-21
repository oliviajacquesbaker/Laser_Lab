using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffractionGrid : BoardObject
{
    DiffractionGrid()
    {
        Rotation = 0;
    }

    DiffractionGrid(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Rotation = rotation;
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

        if ((num == 0 && (Rotation == 0 || Rotation == 2)) || (num == 1 && (Rotation == 1 || Rotation == 3)))
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
        Laser.Direction first25Dir = getNewDirection(laser, 0);
        Laser.Direction second25Dir = getNewDirection(laser, 1);
        Laser first25 = new Laser(newOrigin, first25Dir, (laser.red/4), (laser.green/4), (laser.blue/4));
        Laser mid50 = new Laser(newOrigin, laser.direction, (laser.red/2), (laser.green/2), (laser.blue/2));
        Laser second25 = new Laser(newOrigin, second25Dir, (laser.red/4), (laser.green/4), (laser.blue/4));

        Laser[] returning = new Laser[3];
        returning[0] = first25;
        returning[1] = mid50;
        returning[2] = second25;
        return returning;

    }
}
