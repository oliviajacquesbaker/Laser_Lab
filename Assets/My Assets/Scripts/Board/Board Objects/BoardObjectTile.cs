using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObjectTile : BoardObject
{
    public BoardObjectTile()
    {
        Orientation = 0;
    }

    /*
    public BoardObjectTile(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Orientation = rotation;
    }


    public override Laser[] OnLaserHit(Laser laser)
    {
        Laser newLaser = new Laser(laser.direction, laser.red, laser.green, laser.blue);

        return new Laser[] { newLaser };
    }
    */

    public override Laser[] OnLaserHit(Laser laser)
    {
        return null;
    }
}
