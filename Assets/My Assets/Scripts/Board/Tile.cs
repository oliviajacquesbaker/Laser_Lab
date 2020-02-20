using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : BoardObject
{
    Tile()
    {
        Rotation = 0;
    }

    Tile(int rotation)
    {
        if (rotation >= 0 && rotation <= 3)
            Rotation = rotation;
    }


    public override Laser[] OnLaserHit(Laser laser)
    {
        Vector2Int newOrigin = new Vector2Int((int)transform.position.x, (int)transform.position.y);
        Laser newLaser = new Laser(newOrigin, laser.direction, laser.red, laser.green, laser.blue);

        Laser[] returning = new Laser[1];
        returning[0] = newLaser;
        return returning;
    }
}
